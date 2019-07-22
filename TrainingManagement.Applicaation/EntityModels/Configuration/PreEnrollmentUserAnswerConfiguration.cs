using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class PreEnrollmentUserAnswerConfiguration : IEntityTypeConfiguration<PreEnrollmentUserAnswer>
    {
        public void Configure(EntityTypeBuilder<PreEnrollmentUserAnswer> builder)
        {
            builder.ToTable("PreEnrollmentUserAnswer").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.QuestionId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Answer).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.Comments).HasColumnType("nvarchar(250)").IsRequired();
            builder.Property(x => x.TimeTaken).HasColumnType("nvarchar(20)");

            builder.HasOne(s => s.CourseEnrollment).WithMany(t => t.PreEnrollmentUserAnswers).HasForeignKey(s => s.CourseEnrollmentId);
        }
    }
}
