using KayraExport.Application.Dtos.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Features.Commands.Login
{
    public class LoginCommandResponse
    { 
        public AccessToken AccessToken { get; set; }

        public string Message { get; set; }

    }
}
