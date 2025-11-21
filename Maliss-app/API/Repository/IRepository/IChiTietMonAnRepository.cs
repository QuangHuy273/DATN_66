using API.Models;
using API.Models.DTO;

namespace API.Repository.IRepository
{
    public interface IChiTietMonAnRepository : IRepository<ChiTietMonAn , Guid>
    {
        Task<IEnumerable<ChiTietMonAn>> GetMonAnId(string Id);

        Task<IEnumerable<ChiTietMonAn>> GetAll();
    }
}
