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

        public async Task<PagedData<User>> GetPageAsync(int pageIndex, int pageSize, string nameFilter = null)
        {
            if (!string.IsNullOrEmpty(nameFilter))
            {
                return await _unitOfWork.GetRepository<User, Guid, UserRepository>()
                    .GetPageAsync(pageIndex, pageSize, user => user.UserName.Contains(nameFilter));
            }

            return await _unitOfWork.GetRepository<User, Guid, UserRepository>()
                .GetPageAsync(pageIndex, pageSize);
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<User, Guid, UserRepository>().GetByIdAsync(id);
        }
    }
}
