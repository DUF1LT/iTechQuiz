using System;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories;
using iTechArt.Repositories.Lists;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Foundation.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;


        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<PagedData<User>> GetPageAsync(int pageIndex, int pageSize)
        {
            var paginatedQuery = _unitOfWork.GetRepository<User, Guid, UserRepository>().GetUsers();

            var count = await paginatedQuery.CountAsync();

            var items = await paginatedQuery.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<User>(items, count, pageIndex, pageSize);
        }
    }
}
