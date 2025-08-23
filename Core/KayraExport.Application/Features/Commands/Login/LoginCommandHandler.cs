using KayraExport.Application.Abstractions.Services;
using MediatR;

namespace KayraExport.Application.Features.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }


        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken) 
            => new(){
                AccessToken = await _authService
                    .LoginAsync(request.Email, request.Password),
                Message = "Başarılı bir şekilde giriş yapıldı."
            };

    }
}
