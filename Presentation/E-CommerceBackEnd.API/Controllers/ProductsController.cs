﻿using E_CommerceBackEnd.Application.Abstractions.Storage;
using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Application.RequestParameters;
 
using E_CommerceBackEnd.Application.ViewModels.Products;
using E_CommerceBackEnd.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_CommerceBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IWebHostEnvironment _webHostEnvironment;
        
        readonly private IFileWriteRepository _fileWriteRepository;
        readonly private IFileReadRepository _fileReadRepository;

        readonly private IProductImageFileReadRepository _productImageFileReadRepository;   
        readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly private IInvoiceFileReadRepository _invoiceFileReadRepository; 
        readonly private IInvoiceFileWriteRepository    _invoiceFileWriteRepository;

        readonly private IStorageService _storageService;   


        public ProductsController(IStorageService storageService,IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
       
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;

        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination) 
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products=_productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take( pagination.Size)
                .Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.CreatedDate,
                p.UpdatedDate

            }).ToList();
            return Ok(new
            {
                products,
                totalCount
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id,false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            
            //test
            await _productWriteRepository.AddAsync(new Product
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {

            Product product =await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock=model.Stock;
            product.Name=model.Name;
            product.Price=model.Price;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok(); 
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var datas = await _storageService.UploadAsync("resource/product-images", Request.Form.Files);



            //var datas = await _fileService.UploadAsync("resource/product-images", Request.Form.Files);

             
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage=_storageService.StoreName
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();


            //var datas = await _fileService.UploadAsync("resource/invoices", Request.Form.Files);
            //await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //    Price = 10
            //}).ToList()); 
            //await _invoiceFileWriteRepository.SaveAsync();

            //var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files);
            //await _fileWriteRepository.AddRangeAsync(datas.Select(d => new E_CommerceBackEnd.Domain.Entities.File()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,

            //}).ToList());
            //await _fileWriteRepository.SaveAsync();

            //var f1 = _fileReadRepository.GetAll(false);
            //var p1= _productImageFileReadRepository.GetAll(false);
            //var i1=_invoiceFileReadRepository.GetAll(false);    


            return Ok();
        }

    }
}
 