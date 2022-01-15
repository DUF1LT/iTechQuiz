using System.Linq;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories.UnitOfWork;
using iTechArt.Repositories.Lists;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Foundation.Services
{
    public class PaginatedUserService
    {
        private readonly UnitOfWork _unitOfWork;


        public PaginatedUserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<PaginatedList<User>> GetPageAsync(int pageIndex, int pageSize)
        {
            var paginatedQuery = _unitOfWork.UserRepository.GetQuery();
            var count = await paginatedQuery.CountAsync();

            var items = await paginatedQuery.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<User>(items, count, pageIndex, pageSize);
        }
    }
}
