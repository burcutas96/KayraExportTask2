using KayraExport.Domain.Exceptions.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Domain.Exceptions.Product
{
    public class ProductNameAlreadyExistsException : BadRequestBaseException
    {
        /// <summary>
        /// Bu ürün ismi daha önce kullanılmış.
        /// </summary>
        public ProductNameAlreadyExistsException() : base("Bu ürün ismi daha önce kullanılmış.")
        {
        }
    }
}
