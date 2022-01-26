using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> 
        where TEntity : class, IEntity<TId>, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;


        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }


        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TId id)
        {
            _dbSet.Remove(new TEntity
            {
                Id = id
            });
        }
    }
}
