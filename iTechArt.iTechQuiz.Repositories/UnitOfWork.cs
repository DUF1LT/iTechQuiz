using System.Threading.Tasks;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Repositories
{
    public class UnitOfWork
    {
        private readonly iTechQuizContext _context;

        private UserRepository _userRepository;


        public TRepository GetRepository<TEntity, TId, TRepository>()
            where TEntity : class, IEntity<TId>, new() 
            where TRepository : Repository<TEntity, TId>
        {
            get
            {
                return _repositories[typeof(TEntity)] as TRepository;
            }

            var repository = (TRepository)Activator.CreateInstance(typeof(TRepository), args: _context);
            _repositories.Add(typeof(TEntity), repository);

        public UnitOfWork(iTechQuizContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
