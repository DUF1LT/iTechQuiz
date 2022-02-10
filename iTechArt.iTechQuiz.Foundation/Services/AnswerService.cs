using System;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Foundation.Services
{
    public class AnswerService
    {
        private readonly UnitOfWork _unitOfWork;

        public AnswerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveAnswerAsync(Answer answer)
        {
           await _unitOfWork.GetRepository<Answer, Guid, Repository<Answer, Guid>>().CreateAsync(answer);
           await _unitOfWork.SaveAsync();
        }

    }
}
