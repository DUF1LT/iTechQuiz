using System;
using System.Threading.Tasks;

namespace iTechArt.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);

        Task CreateAsync(TEntity entity);

        void Update(TEntity item);

        void Delete(Guid id);
    }
}
