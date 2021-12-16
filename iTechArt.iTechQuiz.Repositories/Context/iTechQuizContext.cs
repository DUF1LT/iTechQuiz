using iTechArt.iTechQuiz.Domain;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Context
{
    public class iTechQuizContext : DbContext
    {
        public DbSet<User> Users;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            Database.EnsureCreated();
        }
    }
}
