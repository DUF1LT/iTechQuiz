using System;
using System.Reflection;
using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Context
{
    public class iTechQuizContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionTypeLookup> QuestionTypes { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<UsersPassSurveys> UsersPassSurveys { get; set; }


        public iTechQuizContext(DbContextOptions options)
            : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        }
    }
}
