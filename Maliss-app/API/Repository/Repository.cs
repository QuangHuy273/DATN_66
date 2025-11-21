using API.Data;
using API.Repository.IRepository; 
using Microsoft.EntityFrameworkCore;


namespace API.Repository
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class
    {
        private readonly DBAppContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DBAppContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<T?> GetByIdAsync(TKey id) => await _dbSet.FindAsync(id);

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}