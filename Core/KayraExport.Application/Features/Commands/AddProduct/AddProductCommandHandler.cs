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

namespace KayraExport.Application.Features.Commands.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommandRequest, AddProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        public AddProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }


        public async Task<AddProductCommandResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _productReadRepository
                .AnyAsync(p => p.Name == request.ProductUpsertDto.Name
                && !p.IsDeleted))
                throw new ProductNameAlreadyExistsException();


            await _productWriteRepository.AddAsync(new()
            {
                Name = request.ProductUpsertDto.Name,
                Price = request.ProductUpsertDto.Price,
                Description = request.ProductUpsertDto.Description,
                Stock = request.ProductUpsertDto.Stock,
                CreateDate = DateTime.UtcNow
            });

            await _productWriteRepository.SaveChangesAsync();

            return new() { Message = "Ürün başarıyla eklendi." };
        }
    }
}
