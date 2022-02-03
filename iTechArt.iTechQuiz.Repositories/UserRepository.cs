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

        public override async Task<PagedData<User>> GetPageAsync(int pageIndex, int pageSize)
        {
            var includedUsers = DbSet
                .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .Include(p => p.Surveys);

            var count = await includedUsers.CountAsync();

            var items = await includedUsers.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<User>(items, count, pageIndex, pageSize);
        }

        public async Task<PagedData<User>> GetFilteredPageAsync(int pageIndex, int pageSize, Expression<Func<User, bool>> filter)
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

            return new PagedData<User>(items, count, pageIndex, pageSize);
        }
    }
}
