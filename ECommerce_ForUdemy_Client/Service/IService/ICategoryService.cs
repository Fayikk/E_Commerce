using ECommerce_ForUdemy_Models;

namespace ECommerce_ForUdemy_Client.Service.IService
{
    public interface ICategoryService 
    {
        public Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
    }
}
