using KayraExport.Application.Abstract;
using KayraExport.Persistence.Concrete;
using KayraExport.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/KayraExport.Api"));
            configurationManager.AddJsonFile("appsettings.json");

            services.AddDbContext<KayraExportContext>(options => 
                options.UseNpgsql(configurationManager.GetConnectionString("PostgreSql")));

            services.AddScoped<IProductService, ProductManager>();
        }
    }
}
