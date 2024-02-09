using Microsoft.AspNetCore.Mvc;
using MediatR;
using Core.Queries;
using Swashbuckle.AspNetCore.Annotations;
using Infrastructure.Models;
using Infrastructure.DTOs;

namespace Api.Controllers
{
    [Route("api/products/read")]
    [ApiController]
    public class ProductReadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ProductStatusCache _productStatusCache;

        public ProductReadController(IMediator mediator, ProductStatusCache productStatusCache)
        {
            _mediator = mediator;
            _productStatusCache = productStatusCache;
        }

        [HttpGet("status")]
        public IActionResult GetProductStatus()
        {
            var productStatusDictionary = _productStatusCache.GetProductStatusDictionary();
            return Ok(productStatusDictionary);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all products",
            Description = "Retrieves a list of all products in the system."
        )]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var query = new ListProductsQuery();
            var products = await _mediator.Send(query);

            return products;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a product by ID",
            Description = "Retrieves a product by its unique ID."
        )]
        [SwaggerResponse(200, "The product was found.")]
        [SwaggerResponse(404, "The product with the specified ID was not found.")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var query = new GetProductQuery { ProductId = id };
            var product = await _mediator.Send(query);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}
