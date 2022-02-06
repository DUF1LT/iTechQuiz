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

        public override async Task<PagedData<Survey>> GetPageAsync(int pageIndex, int pageSize)
        {
            var includedSurveys = DbSet
                .Include(p => p.UsersPassed);

            var count = await includedSurveys.CountAsync();

            var items = await includedSurveys.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<Survey>(items, count, pageIndex, pageSize);
        }

        public async Task<PagedData<Survey>> GetFilteredPageAsync(int pageIndex, int pageSize, Expression<Func<Survey, bool>> filter)
        {
            var includedSurveys = DbSet.Include(p => p.UsersPassed)
                .Where(filter);

            var count = await includedSurveys.CountAsync();

            var items = await includedSurveys.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<Survey>(items, count, pageIndex, pageSize);
        }

        public override Task<Survey> GetByIdAsync(Guid id)
        {
            return DbSet
                .Include(p => p.UsersPassed)
                .Include(p => p.Pages)
                .Include(p => p.Questions)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
