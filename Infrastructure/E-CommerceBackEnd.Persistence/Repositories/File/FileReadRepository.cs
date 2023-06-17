using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<E_CommerceBackEnd.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ECommerceBackEndDbContext context) : base(context)
        {
        }
    }
}
