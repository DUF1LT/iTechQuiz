using System;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Common.Lists;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.Repositories.Entity;
using iTechArt.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Repositories
{
    public class PaginatedRepository<TEntity> : IPaginatedRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly iTechQuizContext _context;
        private readonly DbSet<TEntity> _dbSet;


        public PaginatedRepository(iTechQuizContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }


        public async Task<PaginatedList<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize)
        {
            var count = await _dbSet.CountAsync();
            var items = await _dbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<TEntity>(items, count, pageIndex, pageSize);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
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

        public async Task DeleteAsync(Guid id)
        {
            _dbSet.Remove(new TEntity
            {
                Id = id
            });
            await _context.SaveChangesAsync();
        }
    }
}
