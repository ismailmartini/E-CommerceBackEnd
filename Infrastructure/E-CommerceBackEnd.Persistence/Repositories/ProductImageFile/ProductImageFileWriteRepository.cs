using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Persistence.Contexts;
using E_CommerceBackEnd.Persistence.Repositories;

namespace E_CommerceBackEnd.Persistence.Repositories
{
    public class ProductImageFileWriteRepository : WriteRepository<E_CommerceBackEnd.Domain.Entities.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ECommerceBackEndDbContext context) : base(context)
        {
        }
    }
}

 