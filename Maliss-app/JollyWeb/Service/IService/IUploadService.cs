using API.Models.ViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace JollyWeb.Service.IService
{
    public interface IUploadService
    {
        Task<UploadResult?> UploadImageAsync(IBrowserFile file);
        Task<bool> DeleteImageAsync(string id);
    }

}
