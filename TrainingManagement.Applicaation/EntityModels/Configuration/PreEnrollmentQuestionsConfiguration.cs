using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class PreEnrollmentQuestionsConfiguration : IEntityTypeConfiguration<PreEnrollmentQuestions>
    {
        public void Configure(EntityTypeBuilder<PreEnrollmentQuestions> builder)
        {
            builder.ToTable("PreEnrollmentQuestion").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.TrainingId).HasColumnType("uniqueidentifier");
            builder.Property(x => x.Question).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Answer).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.MinimumPassingMarks).HasColumnType("int");
            builder.Property(x => x.QuestionWeight).HasColumnType("int");
            builder.Property(x => x.MaxAnswerTime).HasColumnType("int");
            builder.Property(x => x.PrerequisiteLinks).HasColumnType("nvarchar(max)");
            builder.Property(x => x.IsActive).HasColumnType("bit").HasDefaultValue(true);

            builder.HasOne(t => t.TrainerSubjectMapping).WithMany(p => p.PreEnrollmentQuestions).HasForeignKey(t => t.TrainingId);
        }
    }
}
