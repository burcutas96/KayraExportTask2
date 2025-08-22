using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities;
using KayraExport.Domain.Exceptions.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Features.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        public DeleteProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }



        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository
                .GetSingleAsync(p => p.Id == request.ProductId && !p.IsDeleted)
                ??
                throw new ProductNotFoundException();

            product.IsDeleted = true;
            product.DeleteDate = DateTime.UtcNow;

            await _productWriteRepository.SaveChangesAsync();

            return new() { Message = "Ürün başarıyla silindi." };
        }
    }
}
