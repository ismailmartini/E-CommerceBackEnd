using E_CommerceBackEnd.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Domain.Entities
{
    public class CompletedOrder:BaseEntities
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
