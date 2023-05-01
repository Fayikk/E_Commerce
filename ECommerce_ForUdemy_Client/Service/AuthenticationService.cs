using Blazored.LocalStorage;
using E_CommerceForUdemy_Common;
using ECommerce_ForUdemy_Client.Service.IService;
using ECommerce_ForUdemy_Models;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce_ForUdemy_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient httpClient,ILocalStorageService localStorageService,AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authStateProvider = authStateProvider;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO signInRequest)
        {
            var content = JsonConvert.SerializeObject(signInRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorageService.SetItemAsync(Keys.Local_Token, result.Token);
                await _localStorageService.SetItemAsync(Keys.Local_UserDetails, result.UserDTO);
                ((CustomStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new LoginResponseDTO() { IsAuthSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync(Keys.Local_Token);
            await _localStorageService.RemoveItemAsync(Keys.Local_UserDetails);

            ((CustomStateProvider)_authStateProvider).NotifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequest)
        {
            var content = JsonConvert.SerializeObject(signUpRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignUpResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new SignUpResponseDTO { IsRegisterationSuccessful = true };
            }
            else
            {
                return new SignUpResponseDTO { IsRegisterationSuccessful = false, Errors = result.Errors };
            }
        }
    }
}
