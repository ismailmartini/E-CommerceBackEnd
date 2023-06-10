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
        public async Task Get() 
        {
           Product p = await _productReadRepository.GetByIdAsync("312bfb55-582c-4451-aa32-ae22b34bd1cc");

            p.Name = "test product interceptor - ";
            await _productWriteRepository.SaveAsync();  

        }


    }
}
 