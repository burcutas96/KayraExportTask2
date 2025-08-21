using KayraExport.Application.Dtos.Product;
using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            List<Product> products = await _productReadRepository
                .GetAllAsync(p => !p.IsDeleted);

            List<ProductDto> productDtos = new();

            products.ForEach(p => productDtos.Add(
                new()
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock
                }));

            return new() { ProductDtos = productDtos };
        }
    }
}
