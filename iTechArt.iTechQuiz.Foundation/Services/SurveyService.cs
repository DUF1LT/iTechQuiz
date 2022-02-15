using System;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Foundation.Services
{
    public class SurveyService
    {
        private readonly UnitOfWork _unitOfWork;


        public SurveyService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveSurveyAsync(Survey survey)
        {
            await _unitOfWork.GetRepository<Survey, Guid, Repository<Survey, Guid>>().CreateAsync(survey);
            await _unitOfWork.SaveAsync();
        }
    }
}
