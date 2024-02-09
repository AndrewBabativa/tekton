using MediatR;
using Infrastructure.Repositories;
using AutoMapper;
using Core.Commands;

namespace Core.Handlers
{
    /// <summary>
    /// Handles the command for updating an existing product.
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommandHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the update product command.
        /// </summary>
        /// <param name="request">The update product command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the update operation.</returns>
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetProductById(request.ProductId);

            if (existingProduct == null)
            {
                return Unit.Value;
            }

            _mapper.Map(request, existingProduct);

            await _productRepository.UpdateProduct(existingProduct);

            return Unit.Value;
        }
    }
}
