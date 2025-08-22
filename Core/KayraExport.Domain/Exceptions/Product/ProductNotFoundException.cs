using KayraExport.Domain.Exceptions.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Domain.Exceptions.Product
{
    public class ProductNotFoundException : NotFoundBaseException
    {
        /// <summary>
        /// Böyle bir ürün bulunamadı.
        /// </summary>
        public ProductNotFoundException() : base("Böyle bir ürün bulunamadı.")
        {
        }
    }
}
