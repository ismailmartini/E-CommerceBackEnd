using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task Get() {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new() {Id=Guid.NewGuid(),Name="test 1",Price=10,Stock=1,CreatedDate=DateTime.UtcNow},
            //    new() {Id=Guid.NewGuid(),Name="test 2",Price=20,Stock=12,CreatedDate=DateTime.UtcNow},
            //    new() {Id=Guid.NewGuid(),Name="test 3",Price=30,Stock=13,CreatedDate=DateTime.UtcNow},
            //    new() {Id=Guid.NewGuid(),Name="test 4",Price=40,Stock=14,CreatedDate=DateTime.UtcNow},
            //    new() {Id=Guid.NewGuid(),Name="test 5",Price=40,Stock=15,CreatedDate=DateTime.UtcNow},

            //});

            //var count= await _productWriteRepository.SaveAsync();


          /// Product p= await _productReadRepository.GetByIdAsync("716f75c4-fdcd-4259-97e2-7c99b68b9038",true);
           Product p = await _productReadRepository.GetByIdAsync("716f75c4-fdcd-4259-97e2-7c99b68b9038", false); //tracking false
            p.Name = "Test Pname";
           await _productWriteRepository.SaveAsync();

        }


    }
}
 