using Core.Commands;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers;
using Moq;
using MediatR;
using Infrastructure.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Test
{
    public class ProductControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CreateProduct_ValidCommand_ReturnsStatusCode201()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductController(mediatorMock.Object);

            var validProductData = new CreateProductCommand
            {
                Name = "Product Name",
                StatusName = "Active",
                Stock = 10,
                Description = "Product Description",
                Price = 49.99m,
                Discount = 5.0m
            };

            // Act
            var result = await controller.CreateProduct(validProductData);

            // Assert
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual(201, (result as StatusCodeResult)?.StatusCode);
        }


        [Test]
        public async Task CreateProduct_InvalidCommand_ReturnsBadRequest()
        {
            var controller = new ProductController(_mediatorMock.Object);
            CreateProductCommand command = null; 

            var result = await controller.CreateProduct(command);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task UpdateProduct_ValidCommand_ReturnsNoContent()
        {
            var options = new DbContextOptionsBuilder<ProductsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>())
                .Build();

            using (var context = new ProductsContext(options, configuration))
            {
                var product = new Product
                {
                    ProductId = 1,
                    Name = "TestProduct",
                    Description = "TestDescription",
                    StatusName = "TestStatusName",
                };

                context.Products.Add(product);
                context.SaveChanges();

                var controller = new ProductController(_mediatorMock.Object);

                var command = new UpdateProductCommand
                {
                    ProductId = 1,
                    NewName = "NewProductName",
                    NewStatusName = "NewStatusName",
                    NewStock = 10,
                    NewDescription = "NewProductDescription",
                    NewPrice = 99.99m,
                    NewDiscount = 10
                };

                var result = await controller.UpdateProduct(command);

                Assert.IsInstanceOf<NoContentResult>(result);
            }
        }


        [Test]
        public async Task UpdateProduct_InvalidCommand_ReturnsBadRequest()
        {
            var controller = new ProductController(_mediatorMock.Object);
            UpdateProductCommand command = null; // Comando inválido

            var result = await controller.UpdateProduct(command);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task DeleteProduct_ExistingProduct_ReturnsNoContent()
        {
            var options = new DbContextOptionsBuilder<ProductsContext>()
              .UseInMemoryDatabase(databaseName: "TestDatabase")
              .Options;

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>())
                .Build();

            using (var context = new ProductsContext(options, configuration))
            {
                var product = new Product
                {
                    ProductId = 1,
                    Name = "TestProduct",
                    Description = "TestDescription",
                    StatusName = "TestStatusName",
                };

                context.Products.Add(product);
                context.SaveChanges();

                var controller = new ProductController(_mediatorMock.Object);
            var productId = 1; 

            var result = await controller.DeleteProduct(productId);

            Assert.IsInstanceOf<NoContentResult>(result);
          }
        }

        [Test]
        public async Task DeleteProduct_NonExistingProduct_ReturnsNotFound()
        {
            var controller = new ProductController(_mediatorMock.Object);
            var productId = 999; 
            var result = await controller.DeleteProduct(productId);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
