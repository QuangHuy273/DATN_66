using API.Models.ViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace Admin.Service.IService
{
    public interface IUploadService
    {
        Task<UploadResult?> UploadImageAsync(IBrowserFile file, string? maMonAn = null, string? chiTiet = null);
        Task<bool> DeleteImageAsync(string fileName);
    }

}
