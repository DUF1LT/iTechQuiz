using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.iTechQuiz.Repositories.Repositories;
using iTechArt.Repositories.Entity;
using iTechArt.Repositories.Repositories;
using iTechArt.Repositories.UnitOfWork;

namespace iTechArt.iTechQuiz.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories;
        private readonly iTechQuizContext _context;


        public UnitOfWork(iTechQuizContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }


        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }
            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
