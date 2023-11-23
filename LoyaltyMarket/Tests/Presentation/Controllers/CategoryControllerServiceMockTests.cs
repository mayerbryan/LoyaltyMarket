using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Domain.Models;
using Domain.Services;
using Xunit;
using Domain.Models.CategoryModels;

namespace Tests.Presentation.Controllers
{
    public class CategoryControllerServiceMockTests
    {
        [Fact]
        public async Task Post_ValidCategory_ReturnsOkResult()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);

            var validCategory = new CategoryRequestModel
            {
                Id = "someId",
                Name = "someName",
                Description = "someDescription"
            };

            var result = await controller.Post(validCategory, CancellationToken.None);

            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Post_InvalidCategory_ReturnsBadRequestResult()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);

            var invalidCategory = new CategoryRequestModel
            {
                Id = "",
                Name = "",
                Description = ""
            };

            var result = await controller.Post(invalidCategory, CancellationToken.None);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResultWithCategories()
        {
            var categoryServiceMock = new Mock<ICategoryService>();

            var categoryResponseModels = new List<CategoryResponseModel>
            {
                new CategoryResponseModel { Id = "1", Name = "Category 1", Description = "Description 1" },
                new CategoryResponseModel { Id = "2", Name = "Category 2", Description = "Description 2" },
            };

            categoryServiceMock
                .Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(categoryResponseModels);

            var controller = new CategoryController(categoryServiceMock.Object);

            var result = await controller.Get(CancellationToken.None);

            var expectedCategories = categoryResponseModels
                .Select(responseModel => new CategoryRequestModel
                {
                    Id = responseModel.Id,
                    Name = responseModel.Name,
                    Description = responseModel.Description
                })
                .ToList();

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedCategories);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var categoryId = "validCategoryId";

            var expectedCategory = new CategoryResponseModel
            {
                Id = categoryId,
                Name = "someName",
                Description = "someDescription"
            };

            categoryServiceMock.Setup(service => service.GetById(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCategory);

            var result = await controller.GetById(categoryId, CancellationToken.None);

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedCategory);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsNotFoundResult()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var invalidCategoryId = "invalidCategoryId";

            categoryServiceMock.Setup(service => service.GetById(invalidCategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((CategoryResponseModel)null);

            var result = await controller.GetById(invalidCategoryId, CancellationToken.None);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Update_ValidIdAndModel_ReturnsOkResult()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var categoryId = "validCategoryId";

            var categoryUpdateModel = new CategoryUpdateModel
            {
                Id = "someId",
                Name = "someName",
                Description = "someDescription"
            };

            var result = await controller.Update(categoryId, categoryUpdateModel, CancellationToken.None);

            result.Should().BeOfType<OkResult>();
            categoryServiceMock.Verify(service => service.UpdateAsync(categoryId, categoryUpdateModel, It.IsAny<CancellationToken>()), Times.Once);;
        }

        [Fact]
        public async Task Update_InvalidId_ReturnsNotFoundResult()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var invalidCategoryId = "invalidCategoryId";

            var categoryUpdateModel = new CategoryUpdateModel
            {
                Id = invalidCategoryId,
                Name = "someName",
                Description = "someDescription"
            };

            categoryServiceMock.Setup(service => service.UpdateAsync(invalidCategoryId, categoryUpdateModel, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Category not found"));

            var result = await controller.Update(invalidCategoryId, categoryUpdateModel, CancellationToken.None);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsOkResult()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var categoryId = "validCategoryId";

            var result = await controller.Delete(categoryId, CancellationToken.None);

            result.Should().BeOfType<OkResult>();
            categoryServiceMock.Verify(service => service.RemoveAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsNotFoundResult()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoryController(categoryServiceMock.Object);
            var invalidCategoryId = "invalidCategoryId";
            categoryServiceMock.Setup(service => service.RemoveAsync(invalidCategoryId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Category not found"));

            var result = await controller.Delete(invalidCategoryId, CancellationToken.None);

            result.Should().BeOfType<NotFoundResult>();
        }


    }
}