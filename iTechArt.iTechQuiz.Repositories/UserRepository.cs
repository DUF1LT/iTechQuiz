using System;
using System.Linq;
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

        public IQueryable<User> GetUsers()
        {
            return DbSet.Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .Include(p => p.Surveys);
        }
    }
}
