using KayraExport.Application.Abstractions.Cache;
using KayraExport.Application.Features.Queries.GetAllProduct;
using KayraExport.Application.Repositories;
using KayraExport.Domain.Exceptions.Product;
using MediatR;

namespace KayraExport.Application.Features.Commands.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommandRequest, AddProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly ICacheService _cacheService;

        public AddProductCommandHandler(
            IProductWriteRepository productWriteRepository, 
            IProductReadRepository productReadRepository, 
            ICacheService cacheService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _cacheService = cacheService;
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

            await _cacheService.RemoveRangeAsync($"{nameof(GetAllProductQueryRequest)}");

            return new() { Message = "Ürün başarıyla eklendi." };
        }
    }
}
