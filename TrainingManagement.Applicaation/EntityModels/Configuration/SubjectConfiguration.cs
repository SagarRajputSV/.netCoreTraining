using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subject").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Name).HasColumnType("nvarchar(100)").IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasColumnType("nvarchar(250)").IsRequired().HasMaxLength(250);
            builder.Property(x => x.Prerequisites).HasColumnType("nvarchar(250)").IsRequired().HasMaxLength(250);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)").HasMaxLength(250);
            builder.Property(x => x.IsFree).HasColumnType("bit").IsRequired().HasDefaultValue(true);
            builder.Property(x => x.IsActive).HasColumnType("bit");
        }
    }
}
