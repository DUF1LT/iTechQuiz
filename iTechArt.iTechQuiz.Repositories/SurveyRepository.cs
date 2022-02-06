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
            var includedUsers = DbSet
                .Include(p => p.UsersPassed);

            var count = await includedUsers.CountAsync();

            var items = await includedUsers.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<Survey>(items, count, pageIndex, pageSize);
        }

        public async Task<PagedData<Survey>> GetFilteredPageAsync(int pageIndex, int pageSize, Expression<Func<User, bool>> filter)
        {
            var includedUsers = DbSet
                .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .Include(p => p.Surveys)
                .Where(filter);

            var count = await includedUsers.CountAsync();

            var items = await includedUsers.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<Survey>(items, count, pageIndex, pageSize);
        }
    }
}
