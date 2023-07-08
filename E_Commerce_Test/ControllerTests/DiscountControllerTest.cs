using E_CommerceForUdemy_API.Controllers;
using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess;
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
    public class DiscountControllerTest
    {

        private readonly IDiscountService _discountService;
        private DiscountController _controller;
        [SetUp]
        public void Setup()
        {
            // Controller'ın bağımlılıklarını burada ayarlayabilirsiniz.
            // Örneğin, _controller = new MyController(mockedCategoryRepository);
            // şeklinde bir mock CategoryRepository kullanabilirsiniz.
            _controller = new DiscountController(_discountService);
        }


        [Test]
        public async Task CreateCoupon_WhenValidDiscount_ReturnsOkResultWithResultData()
        {
            // Arrange
            var discount = new Discount
            {
                CouponCode = "ABC123",
                AmountDiscount = 10
            };

            // Act
            var result = await _controller.CreateCoupon(discount) as OkObjectResult;
            var response = result?.Value as Discount;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(discount.CouponCode, response.CouponCode);
            Assert.AreEqual(discount.AmountDiscount, response.AmountDiscount);
        }

        [Test]
        public async Task ImplementCoupon_WhenValidCoupon_ReturnsOkResultWithResultData()
        {
            // Arrange
            var couponCode = "ABC123";

            // Act
            var result = await _controller.ImplementCoupon(couponCode) as OkObjectResult;
            var response = result?.Value as string;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(response);
            Assert.AreEqual("Coupon implemented: ABC123", response);
        }

    }
}
