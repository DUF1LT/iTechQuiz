﻿using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.iTechQuiz.Repositories.Configurations
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasMany(e => e.UsersPassed)
                .WithOne(e => e.Survey)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Questions)
                .WithOne(e => e.Survey)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}