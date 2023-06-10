using E_CommerceBackEnd.Application.Repositories;
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
        public async void Get() {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new() {Id=Guid.NewGuid(),Name="test 1",Price=10,Stock=1,CreatedDate=DateTime.UtcNow},
                new() {Id=Guid.NewGuid(),Name="test 2",Price=20,Stock=12,CreatedDate=DateTime.UtcNow},
                new() {Id=Guid.NewGuid(),Name="test 3",Price=30,Stock=13,CreatedDate=DateTime.UtcNow},
                new() {Id=Guid.NewGuid(),Name="test 4",Price=40,Stock=14,CreatedDate=DateTime.UtcNow},
                new() {Id=Guid.NewGuid(),Name="test 5",Price=40,Stock=15,CreatedDate=DateTime.UtcNow},

            });

            var count= await _productWriteRepository.SaveAsync();
        }
    }
}
