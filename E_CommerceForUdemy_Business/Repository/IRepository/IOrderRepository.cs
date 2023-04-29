using E_CommerceForUdemy_DataAccess;
using ECommerce_ForUdemy_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Business.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<OrderDTO> Get(int id);
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId = null,string? status = null);   
        public Task<OrderDTO> Create(OrderDTO orderDTO);
        public Task<int> Delete(int id);
        public Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO orderDTO);
        public Task<OrderHeaderDTO> MarkPaymentSuccessful(int id);
        public Task<bool> UpdateOrderStatus(int orderId, string status);
        
    }
}
