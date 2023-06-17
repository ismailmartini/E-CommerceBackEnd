using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Persistence.Contexts;

namespace E_CommerceBackEnd.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<E_CommerceBackEnd.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ECommerceBackEndDbContext context) : base(context)
        {
        }
    }
} 