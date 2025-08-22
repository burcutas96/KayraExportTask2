using KayraExport.Application.Features.Queries.GetAllProduct;
using KayraExport.Application.Features.Queries.GetById;
using KayraExport.Domain.Exceptions.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KayraExport.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
            => _mediator = mediator;



        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
            => Ok(await _mediator.Send(getAllProductQueryRequest));




        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
            => Ok(await _mediator.Send(getByIdProductQueryRequest));
    }
}
