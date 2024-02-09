using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using System.Threading.Tasks;
using Core.Commands;
using Core.Queries;

namespace Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new product")]
        [SwaggerResponse(201, "Product created successfully")]
        [SwaggerResponse(400, "Invalid product object")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            try
            {
                if (command == null)
                {
                    return BadRequest("Invalid product object");
                }

                await _mediator.Send(command);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update an existing product")]
        [SwaggerResponse(204, "Product updated successfully")]
        [SwaggerResponse(400, "Invalid product object")]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            if (!ModelState.IsValid || command == null)
            {
                return BadRequest("Invalid product object");
            }

            var existingProduct = await _mediator.Send(new GetProductQuery { ProductId = command.ProductId });

            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{productId}")]
        [SwaggerOperation(Summary = "Delete a product")]
        [SwaggerResponse(204, "Product deleted successfully")]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var existingProduct = await _mediator.Send(new GetProductQuery { ProductId = productId });

            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            await _mediator.Send(new DeleteProductCommand { ProductId = productId });

            return NoContent();
        }
    }
}
