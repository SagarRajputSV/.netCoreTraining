using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class TrainerSubjectMappingConfiguration : IEntityTypeConfiguration<TrainerSubjectMapping>
    {
        public void Configure(EntityTypeBuilder<TrainerSubjectMapping> builder)
        {
            builder.ToTable("TrainerSubjectMapping").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.SubjectId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.TrainerId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Instructions).HasColumnType("nvarchar(500)").IsRequired();
            builder.Property(x => x.StartDate).HasColumnType("datetime2(7)").IsRequired();
            builder.Property(x => x.EndDate).HasColumnType("datetime2(7)").IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();

            builder.HasOne(s => s.Subject).WithMany(t => t.TrainerSubjectMappings).HasForeignKey(s => s.SubjectId);
            builder.HasOne(f => f.Trainer).WithMany(t => t.TrainerSubjectMappings).HasForeignKey(f => f.TrainerId);
        }
    }
}
