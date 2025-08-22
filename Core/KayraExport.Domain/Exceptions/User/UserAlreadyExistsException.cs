using KayraExport.Domain.Exceptions.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Domain.Exceptions.User
{
    public class UserAlreadyExistsException : BadRequestBaseException
    {
        /// <summary>
        /// Bu e-posta daha önce kullanılmış!
        /// </summary>
        public UserAlreadyExistsException() : base("Bu e-posta daha önce kullanılmış!")
        {
        }
    }
}
