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
using Microsoft.AspNetCore.Authorization;
using E_CommerceBackEnd.Application.Features.Commands.ChangeShowcaseImage;

namespace E_CommerceBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  

    public class ProductsController : ControllerBase
    { 

        readonly IMediator _mediator;
        public ProductsController(IMediator mediator )
        {
            

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
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
           CreateProductCommandResponse response=  await _mediator.Send(createProductCommandRequest);           
             
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {

           UpdateProductCommandResponse response= await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }



        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok(); 
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
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
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteProductImage([FromQuery,FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            removeProductImageCommandRequest.ImageId = imageId;

            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest); 
            return Ok();
        }


        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery]ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            ChangeShowcaseImageCommandResponse changeShowcaseImageCommandResponse=await  _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(changeShowcaseImageCommandResponse);
        }
    }
}
 