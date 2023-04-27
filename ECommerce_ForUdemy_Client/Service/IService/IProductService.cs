using ECommerce_ForUdemy_Models;

namespace ECommerce_ForUdemy_Client.Service.IService
{
    public interface IProductService
    {
        public  Task<IEnumerable<ProductDTO>> GetAll();
        public Task<ProductDTO> Get(int productId);
            
    }
}
