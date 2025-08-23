using KayraExport.Application.Abstractions.Jwt;
using KayraExport.Application.Abstractions.Services;
using KayraExport.Application.Dtos.Jwt;
using KayraExport.Domain.Entities;
using KayraExport.Domain.Exceptions.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly IUserService _userService;
        readonly ITokenService _tokenService;

        public AuthService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        public async Task<AccessToken> LoginAsync(string email, string password)
        {
            User user = await _userService
                .FindByEmailAsync(email);

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new UserNotFoundException();

            return _tokenService.CreateToken(user);
        }
    }
}
