using System.Threading.Tasks;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.iTechQuiz.Repositories.Repositories;

namespace iTechArt.iTechQuiz.Repositories.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly iTechQuizContext _context;

        private UserRepository _userRepository;


        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }


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
