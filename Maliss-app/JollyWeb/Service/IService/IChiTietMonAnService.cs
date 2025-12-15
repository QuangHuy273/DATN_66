
using API.Models.DTO;

namespace JollyWeb.Service.IService
{
    public interface IChiTietMonAnService : IApiService
    {
        Task<List<ChiTietMonAnDTO>> GetChiTiet(string Id);
        Task<List<ChiTietMonAnDTO>> GetAll();

        /// <summary>
        /// Lấy chi tiết món ăn đang hoạt động (dành cho khách hàng)
        /// </summary>
        Task<List<ChiTietMonAnDTO>> GetActiveDetails(string monAnId);
        
        /// <summary>
        /// Lấy tất cả chi tiết món ăn đang hoạt động
        /// </summary>
        Task<List<ChiTietMonAnDTO>> GetAllActiveDetails();
    }
}
