using System;
using System.Threading.Tasks;
using iTechArt.Repositories.Entity;

namespace iTechArt.Repositories.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(Guid id);

        Task CreateAsync(TEntity entity);

        void Update(TEntity item);

        Task DeleteAsync(Guid id);
    }
}
