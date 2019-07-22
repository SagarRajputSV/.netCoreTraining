using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class CourseEnrollmentConfiguration : IEntityTypeConfiguration<CourseEnrollment>
    {
        public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
        {
            builder.ToTable("CourseEnrollment").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.UserId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("bit");
            builder.Property(x => x.IsPreEnrollmentLinkVisited).HasColumnName("IsPreEnrollmentLinkVisited").HasColumnType("bit").HasDefaultValue(false);

            builder.HasOne(s => s.TrainerSubjectMappings).WithMany(t => t.CourseEnrollments).HasForeignKey(s => s.TrainingId);
        }
    }
}
