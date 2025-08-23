using KayraExport.Application.Abstractions.Cache;
using MediatR;

namespace KayraExport.Application.Behaviors.Cache
{
    public class RedisCacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        readonly ICacheService _cacheService;

        public RedisCacheBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Sadece query’leri cacheliyoruz.
            if (request is ICacheableQuery cacheableQuery) 
            {
                string cacheKey = cacheableQuery.GetCacheKey();

                TResponse? cachedData = await _cacheService
                    .GetAsync<TResponse>(cacheKey);

                if (cachedData != null)
                    return cachedData;


                TResponse? response = await next();

                if (response != null)
                    await _cacheService
                        .SetAsync(cacheKey, response, DateTime.Now.AddMinutes(5));

                return response;
            }

            return await next();
        }
    }
}
