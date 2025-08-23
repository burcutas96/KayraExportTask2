using KayraExport.Application.Abstractions.Jwt;
using KayraExport.Application.Dtos.Jwt;
using KayraExport.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KayraExport.Infrastructure.Services.Jwt
{
    public class JwtService : ITokenService
    {
        readonly JwtSettings _jwtSettings;
        DateTime _accessTokenExpiration;
         
        public JwtService(IConfiguration configuration)
        {
            _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
        }



        public AccessToken CreateToken(User user)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512Signature);
            _accessTokenExpiration = DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpiration);
            
            JwtSecurityToken jwt = CreateJwtSecurityToken(_jwtSettings, user, signingCredentials);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            string token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new()
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }


        public JwtSecurityToken CreateJwtSecurityToken(JwtSettings jwtSettings, User user, SigningCredentials signingCredentials)
            => new
                (issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: new List<Claim> { new(ClaimTypes.Name, user.FirstName) },
                signingCredentials: signingCredentials);
            
    }
}
