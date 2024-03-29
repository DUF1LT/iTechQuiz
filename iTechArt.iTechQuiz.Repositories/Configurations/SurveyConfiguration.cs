﻿using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasMany(e => e.UsersPassed)
                .WithOne(e => e.Survey);

            builder.Property(e => e.Title)
                .HasMaxLength(50);
        }
    }
}