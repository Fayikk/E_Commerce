using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceForUdemy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(Discount discount)
        {
            var result = await _discountService.CouponCode(discount);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ImplementCoupon(string coupon)
        {
            var result = await _discountService.ImplementCoupon(coupon);
            return Ok(result);
        }
    }
}
