using KayraExport.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Features.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        readonly IUserService _userService;

        public RegisterCommandHandler(IUserService userService)
        {
            _userService = userService;
        }



        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.CreateUserAsync(new()
            { 
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
            });

            return new() { Message = "Kullanıcı kaydı başarıyla oluşturuldu." };
        }
    }
}
