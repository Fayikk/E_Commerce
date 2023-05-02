using ECommerce_ForUdemy_Models;

namespace ECommerce_ForUdemy_Client.Service.IService
{
    public interface IPaymentService
    {
        public Task<SuccessModelDTO> Checkout(StripePaymentDTO model);
    }
}
