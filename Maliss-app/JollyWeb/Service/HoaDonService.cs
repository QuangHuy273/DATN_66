using JollyWeb.Service.IService;
using API.Repository.IRepository;
using API.Models;

namespace JollyWeb.Service
{
    public class HoaDonService : IHoaDonService
    {
        private readonly IHoaDonRepository hoaDonRepository;

        public HoaDonService(IHoaDonRepository hoaDonRepository)
        {
            this.hoaDonRepository = hoaDonRepository;
        }

        public async Task<List<LichSuTrangThai>> GetLichSuTrangThaiByHoaDon(string hoaDonId)
        {
            return await hoaDonRepository.GetLichSu(hoaDonId);
        }

        public async Task<bool> LichSuTrangThai(Guid orderId, string newStatus, string reason)
        {
            try
            {
                await hoaDonRepository.UpdateOrderStatus(orderId, newStatus, reason);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
