using E_CommerceBackEnd.Application.Repositories;

namespace E_CommerceBackEnd.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandlerBase
    {
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    }
}