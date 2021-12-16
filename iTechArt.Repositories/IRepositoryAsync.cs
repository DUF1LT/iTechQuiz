using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTechArt.Repositories
{
    interface IRepositoryAsync<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        void CreateAsync(T item);
        void UpdateAsync(T item);
        void DeleteAsync(int id);
        void SaveAsync();
    }
}
