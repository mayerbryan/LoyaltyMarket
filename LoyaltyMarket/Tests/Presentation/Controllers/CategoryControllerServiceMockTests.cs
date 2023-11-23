using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Domain.Models;
using Domain.Services;
using Xunit;

namespace Tests.Presentation.Controllers
{
    public class CategoryControllerServiceMockTests
    {
        [Fact]
        public async Task Post_ValidCategory_ReturnsOkResult()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);

            // Provide values for the required members
            var validCategory = new CategoryRequestModel
            {
                Id = "someId",
                Name = "someName",
                Description = "someDescription"
            };

            // Act
            var result = await controller.Post(validCategory);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Post_InvalidCategory_ReturnsBadRequestResult()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);

            // Provide values for the required members
            var invalidCategory = new CategoryRequestModel
            {
                Id = "",
                Name = "",
                Description = ""
            };

            // Act
            var result = await controller.Post(invalidCategory);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResultWithCategories()
        {
           // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            // Your test data for response model
            var categoryResponseModels = new List<CategoryResponseModel>
            {
                new CategoryResponseModel { Id = "1", Name = "Category 1", Description = "Description 1" },
                new CategoryResponseModel { Id = "2", Name = "Category 2", Description = "Description 2" },
                // Add more as needed
            };

            // Mock the service to return the response models
            categoryServiceMock
                .Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(categoryResponseModels);

            var controller = new CategoryController(categoryServiceMock.Object);

            // Act
            var result = await controller.Get(CancellationToken.None);

            // Convert response models to request models
            var expectedCategories = categoryResponseModels
                .Select(responseModel => new CategoryRequestModel
                {
                    Id = responseModel.Id,
                    Name = responseModel.Name,
                    Description = responseModel.Description
                })
                .ToList();

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedCategories);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var categoryId = "validCategoryId";

            // Provide values for the required members
            var expectedCategory = new CategoryResponseModel
            {
                Id = categoryId,
                Name = "someName",
                Description = "someDescription"
            };

            categoryServiceMock.Setup(service => service.GetById(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCategory);

            // Act
            var result = await controller.GetById(categoryId);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedCategory);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var invalidCategoryId = "invalidCategoryId";

            // Setup service to return null for an invalid category ID
            categoryServiceMock.Setup(service => service.GetById(invalidCategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((CategoryResponseModel)null);

            // Act
            var result = await controller.GetById(invalidCategoryId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Update_ValidIdAndModel_ReturnsOkResult()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var categoryId = "validCategoryId";

            // Provide values for the required members
            var categoryUpdateModel = new CategoryUpdateModel
            {
                Id = "someId",
                Name = "someName",
                Description = "someDescription"
            };

            // Act
            var result = await controller.Update(categoryId, categoryUpdateModel);

            // Assert
            result.Should().BeOfType<OkResult>();
            categoryServiceMock.Verify(service => service.UpdateAsync(categoryId, categoryUpdateModel, It.IsAny<CancellationToken>()), Times.Once);;
        }

        [Fact]
        public async Task Update_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var invalidCategoryId = "invalidCategoryId";

            // Provide values for the required members
            var categoryUpdateModel = new CategoryUpdateModel
            {
                Id = invalidCategoryId,
                Name = "someName",
                Description = "someDescription"
            };

            categoryServiceMock.Setup(service => service.UpdateAsync(invalidCategoryId, categoryUpdateModel, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Category not found"));

            // Act
            var result = await controller.Update(invalidCategoryId, categoryUpdateModel);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsOkResult()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var categoryId = "validCategoryId";

            // Act
            var result = await controller.Delete(categoryId);

            // Assert
            result.Should().BeOfType<OkResult>();
            categoryServiceMock.Verify(service => service.RemoveAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var invalidCategoryId = "invalidCategoryId";
            categoryServiceMock.Setup(service => service.RemoveAsync(invalidCategoryId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Category not found"));

            // Act
            var result = await controller.Delete(invalidCategoryId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }


    }
}