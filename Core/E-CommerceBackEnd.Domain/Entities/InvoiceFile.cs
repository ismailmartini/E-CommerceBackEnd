using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Domain.Entities
{
    public class InvoiceFile:File
    {
        // ef core table per hierarchy
        public decimal Price { get; set; }
    }
}
