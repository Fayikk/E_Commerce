namespace ECommerce_ForUdemy_Client.Service.IService
{
    public interface IDiscountService
    {
        Task<bool> ImplementCouponCode(string code);
    }
}
