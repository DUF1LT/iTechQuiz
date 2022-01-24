using System.Threading.Tasks;
using iTechArt.iTechQuiz.Repositories.Context;

namespace iTechArt.iTechQuiz.Repositories
{
    public class UnitOfWork
    {
        private readonly iTechQuizContext _context;


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
