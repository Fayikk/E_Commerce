using E_CommerceForUdemy_Business.RabbitMQSender;
using E_CommerceForUdemy_DataAccess.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceForUdemy_DataAccess;
using E_CommerceForUdemy_Business.Repository;
using System.Linq.Expressions;

namespace E_Commerce_Test.RepositoryTests
{
    public class DiscountRepositoryTest
    {
        [Test]
        public async Task CouponCode_Should_AddDiscountToDatabaseAndSendMessage()
        {
            // Arrange
            var discount = new Discount { CouponCode = "TEST123", AmountDiscount = 10 }; 
            var contextMock = new Mock<ApplicationDbContext>();
            var discountsDbSetMock = new Mock<DbSet<Discount>>(); 
            contextMock.SetupGet(db => db.Discounts).Returns(discountsDbSetMock.Object); 
            discountsDbSetMock.Setup(d => d.Add(It.IsAny<Discount>())).Returns(Mock.Of<EntityEntry<Discount>>());
            var messageSenderMock = new Mock<IRabbitMQMessageSender>();

            var service = new DiscountService(contextMock.Object, messageSenderMock.Object);

            // Act
            var result = await service.CouponCode(discount);

            // Assert
            Assert.AreEqual(discount.CouponCode, result); 
            contextMock.Verify(db => db.Add(discount), Times.Once);
            messageSenderMock.Verify(ms => ms.SendMessage(discount, "discountQueue"), Times.Once);
        }

        [Test]
        public async Task ImplementCoupon_Should_ReturnTrue_IfCouponCodeExists()
        {
            // Arrange
            var couponCode = "TEST123";
            var discount = new Discount { CouponCode = couponCode, AmountDiscount = 10 };

            var contextMock = new Mock<ApplicationDbContext>();
            var discountsDbSetMock = new Mock<DbSet<Discount>>();
            contextMock.SetupGet(db => db.Discounts).Returns(discountsDbSetMock.Object);
            discountsDbSetMock.Setup(d => d.FirstOrDefaultAsync(It.IsAny<Expression<Func<Discount, bool>>>(), CancellationToken.None)).ReturnsAsync(discount);

            var service = new DiscountService(contextMock.Object, Mock.Of<IRabbitMQMessageSender>());

            // Act
            var result = await service.ImplementCoupon(couponCode);

            // Assert
            Assert.IsTrue(result);
            contextMock.Verify(db => db.Discounts, Times.Once);
            discountsDbSetMock.Verify(d => d.FirstOrDefaultAsync(It.IsAny<Expression<Func<Discount, bool>>>(), CancellationToken.None), Times.Once);
        }
    }
}
