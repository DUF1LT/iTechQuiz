using System;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Entity;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly iTechQuizContext _context;
        private readonly DbSet<TEntity> _dbSet;


        public Repository(iTechQuizContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
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
