using ECommerce_ForUdemy_Client.Service.IService;
using ECommerce_ForUdemy_Models;
using Newtonsoft.Json;

namespace ECommerce_ForUdemy_Client.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var result = await _httpClient.GetAsync("api/category");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(content);
                return categories;
            }
            return new List<CategoryDTO>(); 
        }
    }
}
