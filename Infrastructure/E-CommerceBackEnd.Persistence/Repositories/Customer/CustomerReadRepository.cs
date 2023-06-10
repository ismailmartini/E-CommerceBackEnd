using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Domain.Entities;
using E_CommerceBackEnd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Persistence.Repositories
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICostumerReadRepository
    {
        public CustomerReadRepository(ECommerceBackEndDbContext context) : base(context)
        {
        }
    }
}
