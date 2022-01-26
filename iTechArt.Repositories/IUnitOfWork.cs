using System.Threading.Tasks;

namespace iTechArt.Repositories
{
    public interface IUnitOfWork
    {
        TRepository GetRepository<TEntity, TId, TRepository>()
            where TRepository : Repository<TEntity, TId>
            where TEntity : class, IEntity<TId>, new();

        Task SaveAsync();
    }
}
