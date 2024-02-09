using MediatR;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Core.Queries;

namespace Core.Handlers
{
    /// <summary>
    /// Handles the query for retrieving a list of products.
    /// </summary>
    public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListProductsQueryHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public ListProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Handles the list products query.
        /// </summary>
        /// <param name="request">The list products query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of products retrieved by the query.</returns>
        public async Task<List<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProducts();

            return (List<Product>)products;
        }
    }
}
