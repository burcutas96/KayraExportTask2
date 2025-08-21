using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Dtos.Product
{
    public class ProductDetailDto : ProductDto
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
