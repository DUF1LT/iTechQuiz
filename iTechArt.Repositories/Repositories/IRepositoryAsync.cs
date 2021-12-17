using System;
using System.Threading.Tasks;

namespace iTechArt.Repositories.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<T> GetAsync(int id);

        void CreateAsync(T item);

        void UpdateAsync(T item);

        void DeleteAsync(int id);

        void SaveAsync();
    }
}
