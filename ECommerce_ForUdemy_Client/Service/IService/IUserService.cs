using E_CommerceForUdemy_DataAccess;

namespace ECommerce_ForUdemy_Client.Service.IService
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
