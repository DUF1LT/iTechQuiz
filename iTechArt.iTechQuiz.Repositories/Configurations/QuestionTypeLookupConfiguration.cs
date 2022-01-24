using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class QuestionTypeLookupConfiguration : IEntityTypeConfiguration<QuestionTypeLookup>
    {
        public void Configure(EntityTypeBuilder<QuestionTypeLookup> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}