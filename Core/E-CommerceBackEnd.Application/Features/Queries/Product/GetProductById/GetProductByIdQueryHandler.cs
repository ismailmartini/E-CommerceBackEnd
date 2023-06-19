using E_CommerceBackEnd.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Application.Features.Queries.Product.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
           E_CommerceBackEnd.Domain.Entities.Product product= await _productReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Name=product.Name,
                Price=product.Price,
                Stock=product.Stock,
            };
        }
    }
}
