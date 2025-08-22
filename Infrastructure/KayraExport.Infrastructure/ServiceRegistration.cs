using KayraExport.Application.Abstractions.Cache;
using KayraExport.Infrastructure.Services.RedisCache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace KayraExport.Infrastructure
{
    public static class ServiceRegistration
    { 
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisCacheSettings>(configuration.GetSection("RedisCacheSettings"));
            services.AddTransient<ICacheService, RedisCacheService>();
        }
    }
}
