using KayraExport.Domain.Exceptions.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Domain.Exceptions.User
{
    public class UserNotFoundException : NotFoundBaseException
    {
        /// <summary>
        /// E-posta veya şifre hatalı!
        /// </summary>
        public UserNotFoundException() : base("E-posta veya şifre hatalı!")
        {
        }
    }
}
