using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Domain.Models;
using Domain.Services;
using Xunit;

namespace Tests.Presentation.Controllers
{
    public class ProductControllerServiceMockTests
    {
        [Fact]
        public async Task Post_ValidProduct_ReturnsOkResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);

            // Provide values for the required members
            var validProduct = new ProductRequestModel
            {
                Id = "someId",
                Name = "someName",
                Description = "someDescription",
                Price = 10.0m,
                CategoryId = "someCategoryId",
                Color = "someColor"
            };

            // Act
            var result = await controller.Post(validProduct);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Post_InvalidProduct_ReturnsBadRequestResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);

            // Provide values for the required members
            var invalidProduct = new ProductRequestModel
            {
                Id = "",
                Name = "",
                Description = "",
                Price = 0.0m,
                CategoryId = "",
                Color = ""
            };

            // Act
            var result = await controller.Post(invalidProduct);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResultWithProducts()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            // Your test data for response model
            var productResponseModels = new List<ProductResponseModel>
            {
                new ProductResponseModel { Id = "1", Name = "Product 1", Description = "Description 1", Price = 10.0m, CategoryId = "1", Color = "Red" },
                new ProductResponseModel { Id = "2", Name = "Product 2", Description = "Description 2", Price = 20.0m, CategoryId = "2", Color = "Blue" },
                // Add more as needed
            };

            // Mock the service to return the response models
            productServiceMock
                .Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(productResponseModels);

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.Get(CancellationToken.None);

            // Convert response models to request models
            var expectedProducts = productResponseModels
                .Select(responseModel => new ProductRequestModel
                {
                    Id = responseModel.Id,
                    Name = responseModel.Name,
                    Description = responseModel.Description,
                    Price = responseModel.Price,
                    CategoryId = responseModel.CategoryId,
                    Color = responseModel.Color
                })
                .ToList();

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedProducts);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var productId = "validProductId";

            // Provide values for the required members
            var expectedProduct = new ProductResponseModel
            {
                Id = productId,
                Name = "someName",
                Description = "someDescription",
                Price = 10.0m,
                CategoryId = "someCategoryId",
                Color = "someColor"
            };

            productServiceMock.Setup(service => service.GetById(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await controller.GetById(productId);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedProduct);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var invalidProductId = "invalidProductId";

            // Setup service to return null for an invalid product ID
            productServiceMock.Setup(service => service.GetById(invalidProductId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ProductResponseModel)null);

            // Act
            var result = await controller.GetById(invalidProductId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Update_ValidIdAndModel_ReturnsOkResult()
        {
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var productId = "validProductId";

            // Provide values for the required members
            var productUpdateModel = new ProductRequestModel
            {
                Id = "someId",
                Name = "someName",
                Description = "someDescription",
                Price = 10.0m,
                CategoryId = "someCategoryId",
                Color = "someColor"
            };

            // Act
            var result = await controller.Update(productId, productUpdateModel);

            // Assert
            result.Should().BeOfType<OkResult>();
            productServiceMock.Verify(service => service.UpdateAsync(productId, productUpdateModel, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var invalidProductId = "invalidProductId";

            // Provide values for the required members
            var productUpdateModel = new ProductRequestModel
            {
                Id = invalidProductId,
                Name = "someName",
                Description = "someDescription",
                Price = 10.0m,
                CategoryId = "someCategoryId",
                Color = "someColor"
            };

            productServiceMock.Setup(service => service.UpdateAsync(invalidProductId, productUpdateModel, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Product not found"));

            // Act
            var result = await controller.Update(invalidProductId, productUpdateModel);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsOkResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var productId = "validProductId";

            // Act
            var result = await controller.Delete(productId);

            // Assert
            result.Should().BeOfType<OkResult>();
            productServiceMock.Verify(service => service.RemoveAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var invalidProductId = "invalidProductId";
            productServiceMock.Setup(service => service.RemoveAsync(invalidProductId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Product not found"));

            // Act
            var result = await controller.Delete(invalidProductId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
