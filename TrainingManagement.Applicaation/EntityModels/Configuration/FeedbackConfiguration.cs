using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.ToTable("Feedback").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.QuestionId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.TraineeRating).HasColumnType("decimal(18,2)").IsRequired().HasDefaultValue(0);
            builder.Property(x => x.TraineeComments).HasColumnType("nvarchar(500)").IsRequired(false);
            builder.Property(x => x.TrainerComments).HasColumnType("nvarchar(500)").IsRequired(false);
            builder.Property(x => x.TrainerRating).HasColumnType("decimal(18,2)").IsRequired().HasDefaultValue(0);

            builder.HasOne(f => f.FeedbackQuestion).WithMany(t => t.Feedbacks).HasForeignKey(f => f.QuestionId);
        }
    }
}
