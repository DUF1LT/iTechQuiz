using System;
using System.Threading.Tasks;

namespace iTechArt.Repositories.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);

        void CreateAsync(TEntity entity);

        void Update(TEntity item);

        void DeleteAsync(int id);
    }
}
