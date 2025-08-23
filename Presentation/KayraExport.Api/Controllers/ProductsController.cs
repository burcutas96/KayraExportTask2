using KayraExport.Api.ResponseModels;
using KayraExport.Application.Features.Commands.AddProduct;
using KayraExport.Application.Features.Commands.DeleteProduct;
using KayraExport.Application.Features.Commands.UpdateProduct;
using KayraExport.Application.Features.Queries.GetAllProduct;
using KayraExport.Application.Features.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KayraExport.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
            => _mediator = mediator;



        [SwaggerOperation(Summary = "Tüm ürünleri listeler.")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllProductQueryResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
            => Ok(await _mediator.Send(getAllProductQueryRequest));




        [SwaggerOperation(Summary = "Belirtilen ürün ID’sine sahip ürünü getirir ve detay bilgilerini döner.")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdProductQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
            => Ok(await _mediator.Send(getByIdProductQueryRequest));




        [SwaggerOperation(Summary = "Yeni ürün ekler.")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddProductCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddProductCommandRequest addProductCommandRequest)
            => Ok(await _mediator.Send(addProductCommandRequest));




        [SwaggerOperation(Summary = "Belirtilen ürün ID’sine sahip ürünü günceller.")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateProductCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        [HttpPut("{ProductId}")]
        public async Task<IActionResult> Update([FromRoute] int ProductId, [FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            updateProductCommandRequest.ProductId = ProductId;

            return Ok(await _mediator.Send(updateProductCommandRequest));
        }




        [SwaggerOperation(Summary = "Belirtilen ürün ID’sine sahip ürünü soft-delete yöntemiyle siler.")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteProductCommandResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
            => Ok(await _mediator.Send(deleteProductCommandRequest));
    }
}
