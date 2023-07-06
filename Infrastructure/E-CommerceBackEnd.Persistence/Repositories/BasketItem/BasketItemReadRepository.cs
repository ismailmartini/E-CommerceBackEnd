using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Domain.Entities;
using E_CommerceBackEnd.Persistence.Contexts;

namespace E_CommerceBackEnd.Persistence.Repositories
{
    public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(ECommerceBackEndDbContext context) : base(context)
        {
        }
    }
}
