using API.Models.DTO;

namespace JollyWeb.Service.IService
{
    public interface IMonAnService : IApiService
    {
        public Task<string> GetIdMonAn();
        public Task<List<MonAnDTO>> GetAll();
    }
}
