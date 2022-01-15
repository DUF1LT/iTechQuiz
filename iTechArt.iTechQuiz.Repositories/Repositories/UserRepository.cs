using System.Linq;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Repositories
{
    public class UserRepository : Repository<User, iTechQuizContext>
    {
        public UserRepository(iTechQuizContext context) : base(context)
        { }


        public override IQueryable<User> GetQuery()
        {
            return DbSet.Include(e => e.UserRoles)
                .ThenInclude(e => e.Role)
                .Include(e => e.Surveys);
        }
    }
}
