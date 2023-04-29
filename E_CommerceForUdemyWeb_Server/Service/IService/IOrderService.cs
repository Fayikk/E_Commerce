using ECommerce_ForUdemy_Models;

namespace E_CommerceForUdemyWeb_Server.Service.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId);
        public Task<OrderDTO> Get(int orderId);
    }
}
