using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Commands
{
    /// <summary>
    /// Represents a command for updating an existing product.
    /// </summary>
    public class UpdateProductCommand : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the Product Id to be updated.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the new name of the product.
        /// </summary>
        [MaxLength(100)]
        public string NewName { get; set; }

        /// <summary>
        /// Gets or sets the new status name of the product.
        /// </summary>
        public string NewStatusName { get; set; }

        /// <summary>
        /// Gets or sets the new stock of the product.
        /// </summary>
        public int NewStock { get; set; }

        /// <summary>
        /// Gets or sets the new description of the product.
        /// </summary>
        [MaxLength(500)]


        public string NewDescription { get; set; }

        /// <summary>
        /// Gets or sets the new price of the product.
        /// </summary>
        public decimal NewPrice { get; set; }

        /// <summary>
        /// Gets or sets the new discount of the product.
        /// </summary>
        public decimal NewDiscount { get; set; }

    }
}
