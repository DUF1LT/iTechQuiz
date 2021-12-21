﻿using System.Threading.Tasks;
using iTechArt.Repositories.Repositories;

namespace iTechArt.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task SaveAsync();
    }
}
