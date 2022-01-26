using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasOne(e => e.Answer)
                .WithOne(e => e.File)
                .HasForeignKey<Answer>(e => e.FileId);

            builder.Property(e => e.Name)
                .HasMaxLength(40);
        }
    }
}