using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class FeedbackQuestionConfiguration : IEntityTypeConfiguration<FeedbackQuestion>
    {
        public void Configure(EntityTypeBuilder<FeedbackQuestion> builder)
        {
            builder.ToTable("FeedbackQuestion").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Question).HasColumnType("nvarchar(550)").IsRequired().HasMaxLength(550);
            builder.Property(x => x.MaxMark).HasColumnType("int").IsRequired();
            builder.Property(x => x.MinMark).HasColumnType("int").IsRequired();
            builder.Property(x => x.IsOptional).HasColumnType("bit");
            builder.Property(x => x.IsActive).HasColumnType("bit");
        }
    }
}
