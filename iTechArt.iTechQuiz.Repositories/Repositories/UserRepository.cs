using System.Linq;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.Repositories.Lists;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Repositories
{
    public class UserRepository : Repository<User, iTechQuizContext>
    {
        public UserRepository(iTechQuizContext context) : base(context)
        { }


        public override IQueryable<User> GetPaginatedQuery(int pageIndex, int pageSize)
        {
            return DbSet.Include(e => e.UserRoles)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
