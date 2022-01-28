using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasMany(e => e.Answers)
                .WithOne(e => e.Question);

            builder.HasOne(e => e.Survey)
                .WithMany(e => e.Questions);

            builder.HasOne<QuestionTypeLookup>()
                .WithMany()
                .HasForeignKey(e => e.Type);

            builder.Property(e => e.Content)
                .HasMaxLength(30);

            builder.Property(e => e.Options)
                .HasMaxLength(100);
        }
    }
}