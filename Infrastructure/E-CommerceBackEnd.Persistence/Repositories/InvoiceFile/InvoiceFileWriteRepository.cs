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
    public class InvoiceFileWriteRepository : WriteRepository<E_CommerceBackEnd.Domain.Entities.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ECommerceBackEndDbContext context) : base(context)
        {
        }
    }
}
