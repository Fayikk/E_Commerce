using ECommerce_ForUdemy_Client.Service.IService;
using Newtonsoft.Json;

namespace ECommerce_ForUdemy_Client.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;
        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;   
        }

        public async Task<bool> ImplementCouponCode(string code)
        {
            var response = await _httpClient.GetAsync("api/discount");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        
        }
    }
}
