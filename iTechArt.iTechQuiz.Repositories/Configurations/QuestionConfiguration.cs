using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.HasMany(e => e.Answers)
                .WithOne(e => e.Question);

            builder.HasOne(e => e.Survey)
                .WithMany(e => e.Questions);

        }
    }
}