using System.Threading.Tasks;

namespace iTechArt.Repositories
{
    public interface IRepository<TEntity, in TId> 
        where TEntity : class, IEntity<TId>, new()
    {
        Task<TEntity> GetUserWithRolesAndSurveysAsync(TId id);

        Task CreateAsync(TEntity entity);

        void Update(TEntity item);

        void Delete(TId id);
    }
}
