using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities;
using KayraExport.Domain.Exceptions.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Features.Queries.GetById
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository
                .GetSingleReadOnlyAsync(p => p.Id == request.ProductId && !p.IsDeleted);

            if (product == null)
                throw new ProductNotFoundException();


            return new() 
            { 
                ProductDetailDto = new() 
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    CreateDate = product.CreateDate,
                    UpdateDate = product.UpdateDate,
                }
            };
        }
    }
}
