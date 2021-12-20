using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.iTechQuiz.Repositories.Repositories;
using iTechArt.Repositories.UnitOfWork;

namespace iTechArt.iTechQuiz.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly iTechQuizContext _context;
        private UserRepository _userRepository;
        public UnitOfWork(iTechQuizContext context)
        {
            _context = context;
        }

        public UserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public async void SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
