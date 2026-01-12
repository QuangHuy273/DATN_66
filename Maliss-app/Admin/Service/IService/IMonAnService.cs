using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface IMonAnService : IApiService
    {
        public Task<List<MonAnDTO>> GetAll();
        public Task<MonAnDTO?> GetById(string id);
        public Task<string> GetIdMonAn();
        public Task<MonAnDTO?> Create(MonAnDTO monAn);
        public Task<bool> Update(MonAnDTO monAn);
        public Task<bool> Delete(string id);
        
        /// <summary>
        /// Filter và phân trang món ăn
        /// </summary>
        public Task<MonAnFilterResponse?> Filter(MonAnFilterRequest filter);
    }
}
