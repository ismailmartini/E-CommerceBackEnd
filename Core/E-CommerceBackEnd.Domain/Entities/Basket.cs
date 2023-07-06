using E_CommerceBackEnd.Domain.Entities.Common;
using E_CommerceBackEnd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Domain.Entities
{
    public class Basket:BaseEntities
    { 
        public string UserId { get; set; }        
        public AppUser User { get; set; }
        public Order Order { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
