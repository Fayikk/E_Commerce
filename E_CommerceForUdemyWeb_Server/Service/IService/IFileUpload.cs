using Microsoft.AspNetCore.Components.Forms;

namespace E_CommerceForUdemyWeb_Server.Service.IService
{
    public interface IFileUpload
    {
        Task<string>UploadFile(IBrowserFile file);
        bool DeleteFile(string filePath);
    }
}
