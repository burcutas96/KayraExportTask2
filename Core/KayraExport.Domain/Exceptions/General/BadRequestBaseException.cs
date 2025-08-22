using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Domain.Exceptions.General
{
    public abstract class BadRequestBaseException : Exception
    {
        protected BadRequestBaseException(string message) : base(message) { }
    }
}
