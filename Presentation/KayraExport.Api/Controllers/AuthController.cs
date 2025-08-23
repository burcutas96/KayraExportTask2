using KayraExport.Api.ResponseModels;
using KayraExport.Application.Features.Commands.Login;
using KayraExport.Application.Features.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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



        [SwaggerOperation(Summary = "Yeni kullanıcı kaydı oluşturur.")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommandRequest registerCommandRequest)
            => Ok(await _mediator.Send(registerCommandRequest));




        [SwaggerOperation(Summary = "Kullanıcı girişi başarılı olursa JWT access token üretilir.")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginCommandResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandRequest loginCommandRequest)
            => Ok(await _mediator.Send(loginCommandRequest));
    }
}
