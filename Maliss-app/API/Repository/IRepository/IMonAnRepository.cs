using API.Models;

namespace API.Repository.IRepository
{
    public interface IMonAnRepository : IRepository <MonAn , string>
    {
        Task<IEnumerable<MonAn>> GetAllAsync();
        Task<MonAn> GetByIdAsync(string id);
    }
}
