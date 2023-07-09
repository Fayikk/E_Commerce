using AutoMapper;
using E_CommerceForUdemy_API.Controllers;
using E_CommerceForUdemy_Business.Repository;
using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess.Data;
using ECommerce_ForUdemy_Models;
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
    public class ProductControllerTest
    {
        private ProductController _controller;
        private IProductRepository _productRepository;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Controller'ın ve repository'nin bağımlılıklarını burada ayarlayabilirsiniz.
            // Örneğin, _productRepository = new ProductRepository(mockedDependencies);
            // şeklinde bir mock ProductRepository kullanabilirsiniz.
            //_productRepository = new ProductRepository(_db,_mapper);

            _controller = new ProductController(_productRepository);
        }

        [Test]
        public async Task GetAll_WhenProductsExist_ReturnsOkResultWithData()
        {
            // Arrange
            var expectedData = new List<ProductDTO>
            {
                new ProductDTO { Id = 1, Name = "Product 1" },
                new ProductDTO { Id = 2, Name = "Product 2" }
            };

            // Act
            var result = await _controller.GetAll() as OkObjectResult;
            var responseData = result?.Value as List<ProductDTO>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(responseData);
            Assert.AreEqual(expectedData.Count, responseData.Count);
            // Diğer özelliklerin de doğrulamalarını yapabilirsiniz.
        }

        [Test]
        public async Task Get_WhenValidId_ReturnsOkResultWithData()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new ProductDTO { Id = 1, Name = "Product 1" };

            // Act
            var result = await _controller.Get(productId) as OkObjectResult;
            var response = result?.Value as ProductDTO;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedProduct.Id, response.Id);
            Assert.AreEqual(expectedProduct.Name, response.Name);
            // Diğer özelliklerin de doğrulamalarını yapabilirsiniz.
        }

        [Test]
        public async Task Get_WhenInvalidId_ReturnsBadRequestWithError()
        {
            // Arrange
            int? productId = null;

            // Act
            var result = await _controller.Get(productId) as BadRequestObjectResult;
            var errorModel = result?.Value as ErrorModelDTO;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(errorModel);
            Assert.AreEqual("Invalid Id", errorModel.ErrorMessage);
            Assert.AreEqual(400, errorModel.StatusCode);
        }

        [Test]
        public async Task GetProductById_WhenValidId_ReturnsOkResultWithData()
        {
            // Arrange
            var categoryId = 1;
                        var expectedProducts = new List<ProductDTO>
                {
                    new ProductDTO { Id = 1, Name = "Product 1" },
                    new ProductDTO { Id = 2, Name = "Product 2" }
                };

            // Act
            var result = await _controller.GetProductById(categoryId) as OkObjectResult;
            var response = result?.Value as List<ProductDTO>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedProducts.Count, response.Count);
            // Diğer özelliklerin de doğrulamalarını yapabilirsiniz.
        }

        [Test]
        public async Task GetProductById_WhenInvalidId_ReturnsBadRequestWithError()
        {
            // Arrange
            var categoryId = 0;

            // Act
            var result = await _controller.GetProductById(categoryId) as BadRequestObjectResult;
            var errorModel = result?.Value as ErrorModelDTO;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(errorModel);
            Assert.AreEqual("Product is not found", errorModel.ErrorMessage);
            Assert.AreEqual(404, errorModel.StatusCode);
        }

    }
}
