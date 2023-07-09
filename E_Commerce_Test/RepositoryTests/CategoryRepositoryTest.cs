using E_CommerceForUdemy_Business.Repository;
using E_CommerceForUdemy_DataAccess;
using E_CommerceForUdemy_DataAccess.Data;
using ECommerce_ForUdemy_Models;
using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceForUdemy_DataAccess.ElasticSearchEntities;
using Nest;
using AutoMapper;

namespace E_Commerce_Test.RepositoryTests
{
    public class CategoryRepositoryTest
    {
        [Test]
        public async Task Create_Should_Return_CreatedCategoryDTO()
        {
            // Arrange
            var categoryDTO = new CategoryDTO { Name = "Test Category" }; 
            var category = new Category(); 

            var dbContextMock = new Mock<ApplicationDbContext>(); 
            var categoriesDbSetMock = new Mock<DbSet<Category>>();
            dbContextMock.SetupGet(db => db.Categories).Returns(categoriesDbSetMock.Object);
            categoriesDbSetMock.Setup(d => d.AddAsync(It.IsAny<Category>(), CancellationToken.None)).ReturnsAsync(Mock.Of<EntityEntry<Category>>()); 
            categoriesDbSetMock.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync(category);
            var mapper = CreateMapper();
            var elasticSearchClientMock = new Mock<ElasticsearchClient>();

            var repository = new CategoryRepository(dbContextMock.Object, mapper, elasticSearchClientMock.Object);

            // Act
            var result = await repository.Create(categoryDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryDTO.Name, result.Name);
            dbContextMock.Verify(db => db.AddAsync(It.IsAny<Category>(), CancellationToken.None), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(CancellationToken.None), Times.Once); 
        }


        [Test]
        public async Task Delete_Should_Return_NumberOfDeletedEntities()
        {
            // Arrange
            var id = 1;

            var dbContextMock = new Mock<ApplicationDbContext>(); 
            var categoriesDbSetMock = new Mock<DbSet<Category>>();
            dbContextMock.SetupGet(db => db.Categories).Returns(categoriesDbSetMock.Object);
            categoriesDbSetMock.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync(Mock.Of<Category>(c => c.Id == id)); 

            var mapper = CreateMapper(); 
            var elasticSearchClientMock = new Mock<ElasticsearchClient>(); 

            var repository = new CategoryRepository(dbContextMock.Object, mapper, elasticSearchClientMock.Object);

            // Act
            var result = await repository.Delete(id);

            // Assert
            Assert.AreEqual(1, result);
            dbContextMock.Verify(db => db.FindAsync<Category>(id), Times.Once); 
            dbContextMock.Verify(db => db.Remove(It.IsAny<Category>()), Times.Once); 
            dbContextMock.Verify(db => db.SaveChangesAsync(CancellationToken.None), Times.Once); 
        }


        [Test]
        public async Task Get_Should_Return_CategoryDTO_IfExists()
        {
            // Arrange
            var id = 1;
            var category = new Category { Id = id, Name = "Test Category" }; 

            var dbContextMock = new Mock<ApplicationDbContext>();
            var categoriesDbSetMock = new Mock<DbSet<Category>>();
            dbContextMock.SetupGet(db => db.Categories).Returns(categoriesDbSetMock.Object);
            categoriesDbSetMock.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync(category);

            var mapper = CreateMapper(); // IMapper nesnesi oluşturun
            var elasticSearchClientMock = new Mock<ElasticsearchClient>(); 

            var repository = new CategoryRepository(dbContextMock.Object, mapper, elasticSearchClientMock.Object);

            // Act
            var result = await repository.Get(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(category.Name, result.Name);
            dbContextMock.Verify(db => db.FindAsync<Category>(id), Times.Once);
        }



        private IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
            });

            return configuration.CreateMapper();
        }
    }
}
