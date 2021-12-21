using System;
using System.Threading.Tasks;

namespace iTechArt.Repositories.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);

        Task CreateAsync(TEntity entity);

        void Update(TEntity item);

        Task DeleteAsync(int id);
    }
}
