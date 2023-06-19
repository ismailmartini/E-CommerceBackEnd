using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryResponse
    {

        public int totalCount { get; set; }

        public object Products { get; set; }
    }
}
