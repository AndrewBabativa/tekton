using Infrastructure.Repositories;
using MediatR;
using AutoMapper;
using Infrastructure.Models;

namespace Core.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(request);
            await _productRepository.AddProduct(newProduct);

            return Unit.Value;
        }
    }
}
