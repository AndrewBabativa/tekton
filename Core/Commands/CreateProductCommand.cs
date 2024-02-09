using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Commands
{
    /// <summary>
    /// Represents a command for creating a new product.
    /// </summary>
    public class CreateProductCommand : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the status name of the product. This field is retrieved from the cache.
        /// </summary>
        [Required]
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the stock of the product.
        /// </summary>
        [Required]
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the discount of the product. This field is obtained from an external service.
        /// </summary>
        public decimal Discount { get; set; }
    }
}
