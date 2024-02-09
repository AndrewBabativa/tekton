using MediatR;
using Infrastructure.Repositories;
using Core.Queries;
using Infrastructure.Models;
using Infrastructure.DTOs;
using Infrastructure.ExternalServices;

namespace Core.Handlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountService _discountService;

        public GetProductQueryHandler(IProductRepository productRepository, IDiscountService discountService)
        {
            _productRepository = productRepository;
            _discountService = discountService;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.ProductId);
            var discount = await _discountService.GetDiscountAsync(request.ProductId.ToString());

            var finalPrice = product.Price * (100 - discount) / 100;
            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Discount = discount,
                FinalPrice = finalPrice
            };

            return productDto;
        }
    }
}
