using System;
using System.Threading.Tasks;
using iTechArt.Common.Lists;
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


        public async Task<PagedData<Survey>> GetPageWithCreatedSurveysAsync(int pageIndex, int pageSize, Guid userId, string nameFilter = null)
        {
            if (!string.IsNullOrEmpty(nameFilter))
            {
                return await _unitOfWork.GetRepository<Survey, Guid, SurveyRepository>()
                    .GetPageWithCreatedSurveysAsync(pageIndex, pageSize, userId, survey => survey.Title.Contains(nameFilter));
            }

            return await _unitOfWork.GetRepository<Survey, Guid, SurveyRepository>()
                .GetPageWithCreatedSurveysAsync(pageIndex, pageSize, userId);
        }

        public async Task<Survey> GetSurveyAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Survey, Guid, SurveyRepository>().GetSurveyAsync(id);
        }
        public async Task<Survey> GetSurveyWithQuestionsAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Survey, Guid, SurveyRepository>().GetSurveyWithQuestionsAsync(id);
        }

        public async Task<Survey> GetSurveyWithQuestionsAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Survey, Guid, SurveyRepository>().GetByIdWithQuestionsAsync(id);
        }

        public async Task<Survey> GetSurveyWithAnswersAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Survey, Guid, SurveyRepository>().GetByIdWithAnswersAsync(id);
        }

        public async Task SaveSurveyAsync(Survey survey)
        {
            await _unitOfWork.GetRepository<Survey, Guid, Repository<Survey, Guid>>().CreateAsync(survey);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateSurveyAsync(Survey survey)
        {
            _unitOfWork.GetRepository<Survey, Guid, Repository<Survey, Guid>>().Update(survey);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteSurveyAsync(Guid id)
        {
            var survey = await GetSurveyAsync(id);
            survey.IsDeleted = true;
            await _unitOfWork.SaveAsync();
        }

    }
}
