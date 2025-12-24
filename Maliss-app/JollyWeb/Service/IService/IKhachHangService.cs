using API.Models;
using API.Models.DTO;

namespace JollyWeb.Service.IService
{
    public interface IKhachHangService : IApiService
    {
        Task<List<KhachHangDTO>> GetAll();

        Task<KhachHangDTO?> GetById(string id);

        Task<bool> Update(KhachHangDTO dto);
        Task<bool> Delete(string id);
        Task<KhachHangDTO?> GetProfile(string khachHangId);
    }
}
