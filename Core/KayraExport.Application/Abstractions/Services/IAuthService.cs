using KayraExport.Application.Dtos.Jwt;
using KayraExport.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<AccessToken> LoginAsync(string email, string password);
    }
}
