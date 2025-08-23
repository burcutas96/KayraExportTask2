using KayraExport.Application.Abstractions.Cache;
using KayraExport.Application.Abstractions.Jwt;
using KayraExport.Infrastructure.Services.Jwt;
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
            services.AddTransient<ITokenService, JwtService>();
        }
    }
}
