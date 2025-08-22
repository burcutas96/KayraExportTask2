using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Domain.Exceptions.General
{
    public abstract class NotFoundBaseException : Exception
    {
        protected NotFoundBaseException(string message) : base(message) { }
    }
}
