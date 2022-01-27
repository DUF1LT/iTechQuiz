using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> 
        where TEntity : class, IEntity<TId>, new()
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;


        public Repository(DbContext context)
        {
            this.Context = context;
            DbSet = this.Context.Set<TEntity>();
        }


        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TId id)
        {
            DbSet.Remove(new TEntity
            {
                Id = id
            });
        }
    }
}
