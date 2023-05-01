using ECommerce_ForUdemy_Models;

namespace ECommerce_ForUdemy_Client.Service.IService
{
    public interface IAuthenticationService
    {
       public Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO requestDTO);
       public Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO);
        Task Logout();
    }
}
