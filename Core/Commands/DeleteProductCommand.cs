using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Commands
{
    /// <summary>
    /// Represents a command for deleting an existing product.
    /// </summary>
    public class DeleteProductCommand : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the Product Id to be deleted.
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }
}
