using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iTechArt.Common.Lists;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories
{
    public class SurveyRepository : Repository<Survey, Guid>
    {
        public SurveyRepository(iTechQuizContext context) : base(context)
        { }


        public async Task<PagedData<Survey>> GetPageWithCreatedSurveysAsync(int pageIndex, int pageSize, Guid userId, Expression<Func<Survey, bool>> filter = null)
        {
            var surveyQuery = DbSet
                .Include(p => p.UsersPassed)
                .Where(e => e.CreatedBy.Id == userId)
                .Include(e => e.CreatedBy)
                .OrderByDescending(e => e.LastModifiedAt)
                .AsQueryable();

            if (filter is not null)
            {
                surveyQuery = surveyQuery.Where(filter);
            }

            var count = await surveyQuery.CountAsync();

            var items = await surveyQuery.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<Survey>(items, count, pageIndex, pageSize);
        }

        public Task<Survey> GetSurveyAsync(Guid id)
        {
            return DbSet
                .Include(p => p.UsersPassed)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
