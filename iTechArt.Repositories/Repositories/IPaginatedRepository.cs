﻿using System.Threading.Tasks;
using iTechArt.Common.Lists;
using iTechArt.Repositories.Entity;

namespace iTechArt.Repositories.Repositories
{
    public interface IPaginatedRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<PaginatedList<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize);
    }
}