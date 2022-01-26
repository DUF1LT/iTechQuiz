using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasOne(e => e.User)
                .WithMany(e => e.Answers);

            builder.HasOne(e => e.File)
                .WithOne(e => e.Answer)
                .HasForeignKey<File>(e => e.AnswerId);

            builder.HasOne(e => e.Question)
                .WithMany(e => e.Answers);

            builder.Property(p => p.Text)
                .HasMaxLength(100);
        }
    }
}