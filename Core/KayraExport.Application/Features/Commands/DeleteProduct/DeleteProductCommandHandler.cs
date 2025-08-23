using KayraExport.Application.Abstractions.Cache;
using KayraExport.Application.Features.Queries.GetAllProduct;
using KayraExport.Application.Features.Queries.GetById;
using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities;
using KayraExport.Domain.Exceptions.Product;
using MediatR;

namespace KayraExport.Application.Features.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly ICacheService _cacheService;

        public DeleteProductCommandHandler(
            IProductReadRepository productReadRepository, 
            IProductWriteRepository productWriteRepository, 
            ICacheService cacheService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _cacheService = cacheService;
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

            await _cacheService.RemoveRangeAsync($"{nameof(GetAllProductQueryRequest)}", 
                $"{nameof(GetByIdProductQueryRequest)}_{request.ProductId}");

            return new() { Message = "Ürün başarıyla silindi." };
        }
    }
}
