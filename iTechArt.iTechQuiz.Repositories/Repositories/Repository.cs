using System;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Repositories.Entity;
using iTechArt.Repositories.Lists;
using iTechArt.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace iTechArt.iTechQuiz.Repositories.Repositories
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        private readonly TContext _context;
        protected readonly DbSet<TEntity> DbSet;


        public Repository(TContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public abstract IQueryable<TEntity> GetQuery();

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            DbSet.Remove(new TEntity
            {
                Id = id
            });
            await _context.SaveChangesAsync();
        }
    }
}
