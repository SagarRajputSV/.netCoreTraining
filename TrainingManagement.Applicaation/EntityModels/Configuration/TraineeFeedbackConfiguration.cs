using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class TraineeFeedbackConfiguration : IEntityTypeConfiguration<TraineeFeedback>
    {
        public void Configure(EntityTypeBuilder<TraineeFeedback> builder)
        {
            builder.ToTable("TraineeFeedback").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();

            builder.Property(x => x.TraineeId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.TrainerId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.QuestionId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.TraineeRating).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Comments).HasColumnType("nvarchar(500)").IsRequired();
            builder.Property(x => x.SubjectId).HasColumnType("uniqueidentifier").IsRequired();

            builder.Property(x => x.TrainingDate).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("bit");

            //builder.HasOne(f => f.Trainee).WithMany(t => t.TraineeFeedbacks).HasForeignKey(f => f.TraineeId);
            //builder.HasOne(f => f.Trainer).WithMany(t => t.TraineeFeedbacks).HasForeignKey(f => f.TrainerId);
            //builder.HasOne(f => f.Feedback).WithMany(t => t.TraineeFeedbacks).HasForeignKey(f => f.FeedbackId);
            builder.HasOne(f => f.Subject).WithMany(t => t.TraineeFeedbacks).HasForeignKey(f => f.SubjectId);

        }
    }
}
