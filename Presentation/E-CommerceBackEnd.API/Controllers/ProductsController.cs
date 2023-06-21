using E_CommerceBackEnd.Application.Abstractions.Storage;
using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Application.RequestParameters;

using E_CommerceBackEnd.Application.ViewModels.Products;
using E_CommerceBackEnd.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;
using MediatR;
using E_CommerceBackEnd.Application.Features.Commands.Product.Createproduct;
using E_CommerceBackEnd.Application.Features.Queries.Product.GetAllProduct;
using E_CommerceBackEnd.Application.Features.Queries.Product.GetProductById;
using E_CommerceBackEnd.Application.Features.Commands.Product.UpdateProduct;
using E_CommerceBackEnd.Application.Features.Commands.Product.RemoveProduct;
using E_CommerceBackEnd.Application.Features.Commands.ProductImageFile.UploadProductImage;
using E_CommerceBackEnd.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using static System.Net.Mime.MediaTypeNames;
using E_CommerceBackEnd.Application.Features.Queries.ProductImageFile.GetProductImages;

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
        readonly private IInvoiceFileWriteRepository _invoiceFileWriteRepository;

        readonly private IStorageService _storageService;
        readonly private IConfiguration _configuration;

        readonly IMediator _mediator;
        public ProductsController(IMediator mediator, IStorageService storageService, IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IConfiguration configuration)
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
            _configuration = configuration;

            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest) 
        {
          GetAllProductQueryResponse response=  await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetProductByIdQueryRequest getProductByIdQueryRequest)
        {
           GetProductByIdQueryResponse response=  await _mediator.Send(getProductByIdQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
           CreateProductCommandResponse response=  await _mediator.Send(createProductCommandRequest);           
             
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {

           UpdateProductCommandResponse response= await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }



        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok(); 
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery,FromForm] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
           UploadProductImageCommandResponse response= await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }


        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromQuery,FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            removeProductImageCommandRequest.ImageId = imageId;

            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest); 
            return Ok();
        }

    }
}
 