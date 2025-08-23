using KayraExport.Application.Dtos.Jwt;
using KayraExport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Abstractions.Jwt
{
    public interface ITokenService 
    {
        AccessToken CreateToken(User user);
    }
}
