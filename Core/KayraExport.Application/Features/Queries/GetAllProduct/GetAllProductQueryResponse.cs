using KayraExport.Application.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public List<ProductDto> ProductDtos { get; set; }
    }
}
