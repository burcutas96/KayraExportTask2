using KayraExport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Persistence.Context
{
    public class KayraExportContext : DbContext
    {
        public KayraExportContext(DbContextOptions options) : base(options)
        {
        }



        public DbSet<Product> Products { get; set; }
    }
}
