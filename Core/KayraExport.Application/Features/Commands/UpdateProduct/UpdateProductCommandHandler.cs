using KayraExport.Application.Dtos.Product;
using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities;
using KayraExport.Domain.Exceptions.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }



        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository
                .GetSingleAsync(p => p.Id == request.ProductId && !p.IsDeleted)
                ??
                throw new ProductNotFoundException();


            if (!string.Equals(product.Name, request.ProductUpsertDto.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (await _productReadRepository
                .AnyAsync(p => p.Name == request.ProductUpsertDto.Name
                && p.Id != request.ProductId
                && !p.IsDeleted))
                throw new ProductNameAlreadyExistsException();
            
                product.Name = request.ProductUpsertDto.Name;
            }

            product.Price = request.ProductUpsertDto.Price;
            product.Description = request.ProductUpsertDto.Description;
            product.Stock = request.ProductUpsertDto.Stock;
            product.UpdateDate = DateTime.UtcNow;

            await _productWriteRepository.SaveChangesAsync();

            return new() { Message = "Ürün başarıyla güncellendi." };
        }
    }
}
