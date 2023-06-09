using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Domain.Entities.Common
{
    public class BaseEntities
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }   
        public DateTime UpdatedDate { get; set; }   

    }
}
