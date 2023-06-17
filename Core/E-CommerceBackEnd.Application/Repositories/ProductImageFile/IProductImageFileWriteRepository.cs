using E_CommerceBackEnd.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Application.Repositories
{
    public interface IProductImageFileWriteRepository : IWriteRepository<E_CommerceBackEnd.Domain.Entities.ProductImageFile>
    {
    }
}

