using System;
using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class UsersPassSurveysConfiguration : IEntityTypeConfiguration<UsersPassSurveys>
    {
        public void Configure(EntityTypeBuilder<UsersPassSurveys> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.HasKey(e => e.Id);

            builder.HasIndex(e =>  new {e.UserId, e.SurveyId})
                .HasFilter($"[UserId] != '{Guid.Empty}'")
                .IsUnique();

            builder.HasOne(e => e.Survey)
                .WithMany(e => e.UsersPassed)
                .HasForeignKey(e => e.SurveyId);

            builder.HasOne(e => e.User)
                .WithMany(e => e.PassedSurveys)
                .HasForeignKey(e => e.UserId);
        }
    }
}