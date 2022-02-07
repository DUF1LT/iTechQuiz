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
    public class UserRepository : Repository<User, Guid>
    {
        public UserRepository(iTechQuizContext context) : base(context)
        { }

        public override async Task<PagedData<User>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<User, bool>> filter = null)
        {
            var includedUsers = DbSet
                .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .Include(p => p.PassedSurveys)
                .Include(p => p.Surveys)
                .AsQueryable();

            if (filter is not null)
            {
                includedUsers = includedUsers.Where(filter);
            }

            var count = await includedUsers.CountAsync();

            var items = await includedUsers.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<User>(items, count, pageIndex, pageSize);
        }

        public override Task<User> GetUserWithRolesAndSurveysAsync(Guid id)
        {
            return DbSet.Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .Include(p => p.Surveys)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
