using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Context
{
    public class iTechQuizContext : DbContext
    {
        public DbSet<User> Users;

        public iTechQuizContext(DbContextOptions options)
        : base(options)
        { }
    }
}
