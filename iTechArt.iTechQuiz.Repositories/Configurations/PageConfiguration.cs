using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasOne(e => e.Survey)
                .WithMany(e => e.Pages);

            builder.HasMany(e => e.Questions)
                .WithOne(e => e.SurveyPage);

            builder.Property(e => e.Name)
                .HasMaxLength(100);
        }
    }
}