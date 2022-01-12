using System;
using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Context
{
    public class iTechQuizContext : IdentityDbContext<User<Guid>, IdentityRole<Guid>, Guid>
    {
        public DbSet<Survey> Survey { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<UsersPassSurveys> UsersPassSurveys { get; set; }


        public iTechQuizContext(DbContextOptions options)
            : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User<Guid>>().ToTable("Users");
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");

            builder.Entity<User<Guid>>()
                .HasMany(e => e.PassedSurveys)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User<Guid>>()
                .HasMany(e => e.Surveys)
                .WithOne(e => e.Founder)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User<Guid>>()
                .HasMany(e => e.Answers)
                .WithOne(e => e.User);

            builder.Entity<Survey>()
                .HasMany(e => e.UsersPassed)
                .WithOne(e => e.Survey)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Survey>()
                .HasMany(e => e.Questions)
                .WithOne(e => e.Survey)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UsersPassSurveys>()
                .HasKey(e => new { e.UserId, e.SurveyId });

            builder.Entity<UsersPassSurveys>()
                .HasOne(e => e.Survey)
                .WithMany(e => e.UsersPassed)
                .HasForeignKey(e => e.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UsersPassSurveys>()
                .HasOne(e => e.User)
                .WithMany(e => e.PassedSurveys)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithOne(e => e.Question)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Question>()
                .HasOne(e => e.Survey)
                .WithMany(e => e.Questions)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Answer>()
                .HasOne(e => e.User)
                .WithMany(e => e.Answers)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Answer>()
                .HasOne(e => e.File)
                .WithOne(e => e.Answer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Answer>()
                .HasOne(e => e.Question)
                .WithMany(e => e.Answers)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<File>()
                .HasOne(e => e.Answer)
                .WithOne(e => e.File)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
