using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using System.Threading.Tasks;
using Core.Commands;
using Core.Queries;

namespace Products.Api.Controllers
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

                // Enviar el comando CreateProductCommand al MediatR
                await _mediator.Send(command);

                // Devolver un código de estado 201 (Created)
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                // Loguear la excepción o manejarla de acuerdo a tus necesidades
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

            // Verificar si el producto existe antes de actualizarlo
            var existingProduct = await _mediator.Send(new GetProductQuery { ProductId = command.ProductId });

            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            // Enviar el comando UpdateProductCommand al MediatR
            await _mediator.Send(command);

            // Devolver un código de estado 204 (No Content)
            return NoContent();
        }

        [HttpDelete("{productId}")]
        [SwaggerOperation(Summary = "Delete a product")]
        [SwaggerResponse(204, "Product deleted successfully")]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            // Verificar si el producto existe antes de eliminarlo
            var existingProduct = await _mediator.Send(new GetProductQuery { ProductId = productId });

            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            // Enviar el comando DeleteProductCommand al MediatR
            await _mediator.Send(new DeleteProductCommand { ProductId = productId });

            // Devolver un código de estado 204 (No Content)
            return NoContent();
        }
    }
}
