using E_CommerceForUdemy_DataAccess;
using ECommerce_ForUdemy_Models;

namespace ECommerce_ForUdemy_Client.Service.IService
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByEmail(string email);

        Task<bool> SendEmail(ChangePasswordModel model);

    }
}
