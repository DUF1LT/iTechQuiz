using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.HasQueryFilter(e => !e.IsSystemUser);

            builder.HasMany(e => e.PassedSurveys)
                .WithOne(e => e.User);

            builder.HasMany(e => e.Surveys)
                .WithOne(e => e.CreatedBy);

            builder.HasMany(e => e.Answers)
                .WithOne(e => e.User);

            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}