using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class UsersPassSurveysConfiguration : IEntityTypeConfiguration<UsersPassSurveys>
    {
        public void Configure(EntityTypeBuilder<UsersPassSurveys> builder)
        {
            builder.HasKey(e => new { e.UserId, e.SurveyId });

            builder.HasOne(e => e.Survey)
                .WithMany(e => e.UsersPassed)
                .HasForeignKey(e => e.SurveyId);

            builder.HasOne(e => e.User)
                .WithMany(e => e.PassedSurveys)
                .HasForeignKey(e => e.UserId);
        }
    }
}