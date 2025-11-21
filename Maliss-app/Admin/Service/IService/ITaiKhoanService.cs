using API.Models.DTO;
using API.Models.ViewModels;

namespace Admin.Service.IService
{
    public interface ITaiKhoanService : IApiService
    {
        public Task<TaiKhoanDTO> Login(LoginViewModel username);
    }
}
