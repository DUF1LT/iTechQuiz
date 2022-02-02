using System;
using System.Threading.Tasks;
using iTechArt.Common.Lists;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories;

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
            return await _unitOfWork.GetRepository<User, Guid, UserRepository>()
                .GetPageAsync(pageIndex, pageSize);
        }
    }
}
