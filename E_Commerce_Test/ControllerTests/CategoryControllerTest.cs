using E_CommerceForUdemy_API.Controllers;
using E_CommerceForUdemy_Business.Repository.IRepository;
using ECommerce_ForUdemy_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Test.ControllerTests
{
    [TestFixture]
    public class CategoryControllerTest
    {
        private CategoryController _categoryController;
        private readonly ICategoryRepository _categoryRepository;
        [SetUp]
        public void Setup()
        {
            // Controller'ın bağımlılıklarını burada ayarlayabilirsiniz.
            // Örneğin, _controller = new MyController(mockedCategoryRepository);
            // şeklinde bir mock CategoryRepository kullanabilirsiniz.
            _categoryController = new CategoryController(_categoryRepository);
        }
        [Test]
        public async Task GetCategories_WhenCategoriesExist_ReturnsOkResultWithData()
        {
            // Arrange
            var expectedData = new List<CategoryDTO>()
            {
                new CategoryDTO { Id = 1, Name = "Category 1" },
                new CategoryDTO { Id = 2, Name = "Category 2" }
            };

            // Act
            var result = await _categoryController.GetCategories() as OkObjectResult;
            var responseData = result?.Value as List<CategoryDTO>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsNotNull(responseData);
            Assert.AreEqual(expectedData.Count, responseData.Count);
            // Diğer özelliklerin de doğrulamalarını yapabilirsiniz.
        }

        [Test]
        public async Task GetCategories_WhenCategoriesDoNotExist_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var expectedErrorMessage = "Category is not found";
            var expectedStatusCode = StatusCodes.Status404NotFound;

            // Act
            var result = await _categoryController.GetCategories() as BadRequestObjectResult;
            var errorModel = result?.Value as ErrorModelDTO;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStatusCode, result.StatusCode);
            Assert.IsNotNull(errorModel);
            Assert.AreEqual(expectedErrorMessage, errorModel.ErrorMessage);
            Assert.AreEqual(expectedStatusCode, errorModel.StatusCode);
        }

    }
}
