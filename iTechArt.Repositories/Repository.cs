using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iTechArt.Common.Lists;
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
            Context = context;
            DbSet = Context.Set<TEntity>();
        }


        public virtual async Task<PagedData<TEntity>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null)
        {
            var count = await DbSet.CountAsync();

            IQueryable<TEntity> pagedItems = DbSet;
            if (filter is not null)
            {
                pagedItems = pagedItems.Where(filter);
            }

            pagedItems = pagedItems.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var items = await pagedItems.ToListAsync();

            return new PagedData<TEntity>(items, count, pageIndex, pageSize);
        }

        public virtual async Task<TEntity> GetUserWithRolesAndSurveysAsync(TId id)
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
