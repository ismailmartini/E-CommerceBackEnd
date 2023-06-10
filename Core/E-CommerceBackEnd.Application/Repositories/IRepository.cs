using E_CommerceBackEnd.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Application.Repositories
{
    public interface IRepository<T> where T:BaseEntities
    {
        DbSet<T> Table { get; }

    }
}
