using System;
using System.Collections.Generic;
using System.Linq;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.iTechQuiz.Repositories.Repositories;
using iTechArt.Repositories.Repositories;
using iTechArt.Repositories.UnitOfWork;

namespace iTechArt.iTechQuiz.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private static Dictionary<Type, object> _repositories;

        private readonly iTechQuizContext _context;


        public UnitOfWork(iTechQuizContext context)
        {
            _context = context;
        }


        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (!_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new GenericRepository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public async void SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
