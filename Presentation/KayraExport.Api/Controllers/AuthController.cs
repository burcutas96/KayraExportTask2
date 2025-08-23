using KayraExport.Application.Features.Commands.Login;
using KayraExport.Application.Features.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KayraExport.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommandRequest registerCommandRequest)
            => Ok(await _mediator.Send(registerCommandRequest));



        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandRequest loginCommandRequest)
            => Ok(await _mediator.Send(loginCommandRequest));
    }
}
