using System;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Foundation.Services
{
    public class FileService
    {
        private readonly UnitOfWork _unitOfWork;


        public FileService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<File> GetFile(Guid id)
        {
            return await _unitOfWork.GetRepository<File, Guid, Repository<File, Guid>>().GetByIdAsync(id);
        }
    }
}
