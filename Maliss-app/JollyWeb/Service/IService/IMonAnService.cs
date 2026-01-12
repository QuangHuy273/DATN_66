using API.Models.DTO;

namespace JollyWeb.Service.IService
{
    public interface IMonAnService : IApiService
    {
        public Task<string> GetIdMonAn();
        public Task<List<MonAnDTO>> GetAll();
        
        /// <summary>
        /// Lấy danh sách món ăn đang hoạt động (dành cho khách hàng)
        /// </summary>
        public Task<List<MonAnDTO>> GetActiveProducts();
        
        /// <summary>
        /// Lấy món ăn theo ID (chỉ active)
        /// </summary>
        public Task<MonAnDTO?> GetActiveProductById(string id);
        
        /// <summary>
        /// Lọc và phân trang sản phẩm cho khách hàng (chỉ sản phẩm hợp lệ)
        /// Sử dụng MonAnFilterRequest - DTO chung với Admin
        /// </summary>
        public Task<MonAnFilterResponse?> FilterForCustomer(MonAnFilterRequest filter);
    }
}
