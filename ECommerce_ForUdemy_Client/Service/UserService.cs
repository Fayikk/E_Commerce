using E_CommerceForUdemy_DataAccess;
using ECommerce_ForUdemy_Client.Service.IService;
using ECommerce_ForUdemy_Models;
using Nest;
using Newtonsoft.Json;
using System.Net.Http;

namespace ECommerce_ForUdemy_Client.Service
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        public UserService(HttpClient client)
        {
            _client = client;
        }
        //https://localhost:7159/api/User/deneme%40gmail.com

        
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var response = await _client.GetAsync($"api/User/{email}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ApplicationUser>(content);
                return user;

            }
            return null;
        }
    }
}
