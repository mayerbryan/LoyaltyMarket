using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Domain.Services;
using Xunit;
using Domain.Models.ProductModels;

namespace Tests.Presentation.Controllers
{
    public class ProductControllerServiceMockTests
    {
        [Fact]
        public async Task Post_ValidProduct_ReturnsOkResult()
        {
            
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            
            var validProduct = new ProductRequestModel
            {
                Id = "someId",
                Name = "someName",
                Description = "someDescription",
                Price = 10.0m,
                CategoryId = "someCategoryId",
                Color = "someColor"
            };
            
            var result = await controller.Post(validProduct);
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Post_InvalidProduct_ReturnsBadRequestResult()
        {
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            
            var invalidProduct = new ProductRequestModel
            {
                Id = "",
                Name = "",
                Description = "",
                Price = 0.0m,
                CategoryId = "",
                Color = ""
            };

            var result = await controller.Post(invalidProduct);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Post_ExcedProductMaxLenght_ReturnsBadRequestResult()
        {
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);

            // Adjust the length of Name and Description to exceed the maximum length
            var invalidProduct = new ProductRequestModel
            {
                Id = "",
                Name = new string('A', 101), // Exceeding the maximum length of 100
                Description = new string('B', 151), // Exceeding the maximum length of 150
                Price = 0.0m,
                CategoryId = "",
                Color = ""
            };

            var result = await controller.Post(invalidProduct);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResultWithProducts()
        {
            var productServiceMock = new Mock<IProductService>();

            var productResponseModels = new List<ProductResponseModel>
            {
                new ProductResponseModel { Id = "1", Name = "Product 1", Description = "Description 1", Price = 10.0m, CategoryId = "1", Color = "Red" },
                new ProductResponseModel { Id = "2", Name = "Product 2", Description = "Description 2", Price = 20.0m, CategoryId = "2", Color = "Blue" },
                
            };

            productServiceMock
                .Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(productResponseModels);

            var controller = new ProductController(productServiceMock.Object);

            var result = await controller.Get(CancellationToken.None);

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

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedProducts);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var productId = "validProductId";

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

            var result = await controller.GetById(productId, CancellationToken.None);

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedProduct);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsNotFoundResult()
        {
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var invalidProductId = "invalidProductId";

            productServiceMock.Setup(service => service.GetById(invalidProductId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ProductResponseModel)null);

            var result = await controller.GetById(invalidProductId, CancellationToken.None);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Update_ValidIdAndModel_ReturnsOkResult()
        {
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var productId = "validProductId";

            var productUpdateModel = new ProductRequestModel
            {
                Id = "someId",
                Name = "someName",
                Description = "someDescription",
                Price = 10.0m,
                CategoryId = "someCategoryId",
                Color = "someColor"
            };

            var result = await controller.Update(productId, productUpdateModel, CancellationToken.None);

            result.Should().BeOfType<OkResult>();
            productServiceMock.Verify(service => service.UpdateAsync(productId, productUpdateModel, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_InvalidId_ReturnsNotFoundResult()
        {
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var invalidProductId = "invalidProductId";

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

            var result = await controller.Update(invalidProductId, productUpdateModel, CancellationToken.None);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsOkResult()
        {
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var productId = "validProductId";

            var result = await controller.Delete(productId, CancellationToken.None);

            result.Should().BeOfType<OkResult>();
            productServiceMock.Verify(service => service.RemoveAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsNotFoundResult()
        {
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);
            var invalidProductId = "invalidProductId";
            productServiceMock.Setup(service => service.RemoveAsync(invalidProductId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Product not found"));

            var result = await controller.Delete(invalidProductId, CancellationToken.None);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
