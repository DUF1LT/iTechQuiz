using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Repositories
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


        public TRepository GetRepository<TEntity, TId, TRepository>()
            where TEntity : class, IEntity<TId>, new()
            where TRepository : Repository<TEntity, TId>
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as TRepository;
            }

            var repository = (TRepository)Activator.CreateInstance(typeof(TRepository), args: _context);
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
