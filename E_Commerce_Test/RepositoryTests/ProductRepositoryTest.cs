using AutoMapper;
using E_CommerceForUdemy_Business.Repository;
using E_CommerceForUdemy_DataAccess.Data;
using ECommerce_ForUdemy_Models.ElasticSearchViewModel;
using ECommerce_ForUdemy_Models;
using Elastic.Clients.Elasticsearch;
using Moq;
using NUnit.Framework;
using E_CommerceForUdemy_DataAccess;
using E_CommerceForUdemy_DataAccess.ElasticSearchEntities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce_Test.RepositoryTests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private Mock<ElasticsearchClient> _elasticSearchClientMock;
        private Mock<IMapper> _mapperMock;
        private ProductRepository _repository;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<ApplicationDbContext>();
            _elasticSearchClientMock = new Mock<ElasticsearchClient>();
            _mapperMock = new Mock<IMapper>();
            _repository = new ProductRepository(_dbContextMock.Object, _mapperMock.Object, _elasticSearchClientMock.Object);
        }

        [Test]
        public async Task Create_Should_ReturnCreatedProductDTO()
        {
            // Arrange
            var productDTO = new ProductDTO();
            var product = new Product();

            _mapperMock.Setup(m => m.Map<ProductDTO, Product>(It.IsAny<ProductDTO>())).Returns(product);
            _dbContextMock.Setup(d => d.Products.AddAsync(It.IsAny<Product>(), CancellationToken.None)).ReturnsAsync(Mock.Of<EntityEntry<Product>>());
            _dbContextMock.Setup(d => d.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);
          

            _mapperMock.Setup(m => m.Map<Product, ProductDTO>(It.IsAny<Product>())).Returns(productDTO);

            // Act
            var result = await _repository.Create(productDTO);

            // Assert
            Assert.AreEqual(productDTO, result);
            _dbContextMock.Verify(d => d.Products.AddAsync(It.IsAny<Product>(), CancellationToken.None), Times.Once);
            _dbContextMock.Verify(d => d.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task Delete_Should_ReturnDeletedProductCount()
        {
            // Arrange
            var productId = 1;
            var product = new Product();

            _dbContextMock.Setup(d => d.Products.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), CancellationToken.None)).ReturnsAsync(product);
            _dbContextMock.Setup(d => d.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            // Act
            var result = await _repository.Delete(productId);

            // Assert
            Assert.AreEqual(1, result);
            _dbContextMock.Verify(d => d.Products.Remove(product), Times.Once);
            _dbContextMock.Verify(d => d.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task Get_Should_ReturnProductDTO()
        {
            // Arrange
            var productId = 1;
            var product = new Product();
            var productDTO = new ProductDTO();

            _dbContextMock.Setup(d => d.Products.Include(p => p.Category).Include(p => p.ProductPrices)
                .FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), CancellationToken.None))
                .ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<Product, ProductDTO>(It.IsAny<Product>())).Returns(productDTO);

            // Act
            var result = await _repository.Get(productId);

            // Assert
            Assert.AreEqual(productDTO, result);
        }

        [Test]
        public async Task GetAll_Should_ReturnProductDTOList()
        {
            // Arrange
            var products = new List<Product> { new Product(), new Product() };
            var productDTOs = new List<ProductDTO> { new ProductDTO(), new ProductDTO() };

            _dbContextMock.Setup(d => d.Products.Include(p => p.Category).Include(p => p.ProductPrices));

            // Act

            var result = await _repository.GetAll();

            // Assert
            Assert.AreEqual(productDTOs, result);
        }

        [Test]
        public async Task GetProductByCategoryId_Should_ReturnProductDTOList()
        {
            // Arrange
            var categoryId = 1;
            var products = new List<Product> { new Product(), new Product() };
            var productDTOs = new List<ProductDTO> { new ProductDTO(), new ProductDTO() };

            _dbContextMock.Setup(d => d.Products.Where(p => p.CategoryId == categoryId));
                //.Returns(Mock.Of<DbSet<Product>>(_ => _.GetEnumerator() == products.GetEnumerator()));
            //_mapperMock.Setup(m => m.Map<List<Product>, List<ProductDTO>>(It.IsAny<List<Product>>())).Returns(productDTOs);

            // Act
            var result = await _repository.GetProductByCategoryId(categoryId);

            // Assert
            Assert.AreEqual(productDTOs, result);
        }

        [Test]
        public async Task Update_Should_ReturnUpdatedProductDTO()
        {
            // Arrange
            var productDTO = new ProductDTO();
            var product = new Product();

            _dbContextMock.Setup(d => d.Products.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), CancellationToken.None)).ReturnsAsync(product);
            _dbContextMock.Setup(d => d.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<Product, ProductDTO>(It.IsAny<Product>())).Returns(productDTO);

            // Act
            var result = await _repository.Update(productDTO);

            // Assert
            Assert.AreEqual(productDTO, result);
            _dbContextMock.Verify(d => d.Products.Update(product), Times.Once);
            _dbContextMock.Verify(d => d.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchAsync_Should_ReturnProductElasticList()
        {
            // Arrange
            var searchViewModel = new ProductSearchViewModel();
            var productElastics = new List<ProductElastic> { new ProductElastic(), new ProductElastic() };

            //_elasticSearchClientMock.Setup(e => e.SearchAsync<ProductElastic>(It.IsAny<Func<SearchDescriptor<ProductElastic>, ISearchRequest>>()))
                //.ReturnsAsync(Mock.Of<ISearchResponse<ProductElastic>>(_ => _.Documents == productElastics));

            // Act
            var result = await _repository.SearchAsync(searchViewModel);

            // Assert
            Assert.AreEqual(productElastics, result);
        }
    }

}
