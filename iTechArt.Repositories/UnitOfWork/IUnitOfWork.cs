using System.Threading.Tasks;

namespace iTechArt.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
