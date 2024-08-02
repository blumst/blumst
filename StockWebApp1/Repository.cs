using Microsoft.EntityFrameworkCore;

namespace StockWebApp1.Interfaces
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => await _dbSet.FindAsync(new object[] { id }, cancellationToken);


        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) 
            => await _dbSet.ToListAsync(cancellationToken);

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
             _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
