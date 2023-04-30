using E_CommerceForUdemy_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Business.Repository.IRepository
{
    public interface IDiscountService
    {
        Task<string> CouponCode(Discount discount);   
        Task<bool> ImplementCoupon(string couponCode);  
    }
}
