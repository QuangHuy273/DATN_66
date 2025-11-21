using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface IMonAnService : IApiService
    {
        public Task<List<MonAnDTO>> GetAll();
        public Task<string> GetIdMonAn();
    }
}
