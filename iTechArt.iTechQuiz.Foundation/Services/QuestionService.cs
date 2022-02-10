using System;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Foundation.Services
{
    public class QuestionService
    {
        private readonly UnitOfWork _unitOfWork;

        public QuestionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Question> GetQuestionAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Question, Guid, Repository<Question, Guid>>().GetByIdAsync(id);
        }
    }
}
