using MediatR;
using Infrastructure.DTOs;

namespace Core.Queries
{
    /// <summary>
    /// Represents a query for retrieving a product by its Id.
    /// </summary>
    public class GetProductQuery : IRequest<ProductDto>
    {
        /// <summary>
        /// Gets or sets the Product Id to be retrieved.
        /// </summary>
        public int ProductId { get; set; }

    }
}
