using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Images>
    {
        public void Configure(EntityTypeBuilder<Images> builder)
        {
            builder.ToTable("Images").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.FileName).HasColumnType("nvarchar(100)").IsRequired().HasMaxLength(100);
            builder.Property(x => x.ImageData).HasColumnType("varbinary(max)").IsRequired().HasMaxLength(100);
            builder.Property(x => x.FileSize).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.ContentType).HasColumnType("nvarchar(150)").IsRequired().HasMaxLength(150);
            builder.Property(x => x.FilePath).HasColumnType("nvarchar(250)").IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.ContainerName).HasColumnType("nvarchar(250)").IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.DirectoryPath).HasColumnType("nvarchar(250)").IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.IsActive).HasColumnType("bit").HasDefaultValue(true);
        }
    }
}
