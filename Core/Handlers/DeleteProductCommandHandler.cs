using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Core.Commands;

namespace Core.Handlers
{
    /// <summary>
    /// Handles the command for deleting an existing product.
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductCommandHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Handles the delete product command.
        /// </summary>
        /// <param name="request">The delete product command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the delete operation.</returns>
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the product from the repository based on the ProductId
            var productToDelete = await _productRepository.GetProductById(request.ProductId);

            if (productToDelete != null)
            {
                // Delete the product from the repository
                await _productRepository.DeleteProduct(productToDelete.ProductId);
            }

            // Additional logic may be added as needed

            return Unit.Value;
        }
    }
}
