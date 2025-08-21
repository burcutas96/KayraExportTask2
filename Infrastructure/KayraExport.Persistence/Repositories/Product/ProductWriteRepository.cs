using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities;
using KayraExport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Persistence.Repositories
{
    internal class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(KayraExportContext context) : base(context)
        {
        }
    }
}
