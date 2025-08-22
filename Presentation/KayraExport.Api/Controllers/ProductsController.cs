using KayraExport.Application.Features.Commands.AddProduct;
using KayraExport.Application.Features.Commands.DeleteProduct;
using KayraExport.Application.Features.Commands.UpdateProduct;
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



         
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddProductCommandRequest addProductCommandRequest)
            => Ok(await _mediator.Send(addProductCommandRequest));




        [HttpPut("{ProductId}")]
        public async Task<IActionResult> Update([FromRoute] int ProductId, [FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            updateProductCommandRequest.ProductId = ProductId;

            return Ok(await _mediator.Send(updateProductCommandRequest));
        }




        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
            => Ok(await _mediator.Send(deleteProductCommandRequest));
    }
}
