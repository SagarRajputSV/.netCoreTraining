using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class ApplicarionVersionConfiguration : IEntityTypeConfiguration<ApplicationVersion>
    {
        public void Configure(EntityTypeBuilder<ApplicationVersion> builder)
        {
            builder.ToTable("Version").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.APIVersion).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(x => x.APIMajorChanges).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.IsAPIVeriosnActive).HasColumnType("bit").IsRequired();
            builder.Property(x => x.UIVersion).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(x => x.UIMajorChanges).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.IsUIVeriosnActive).HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("bit");
        }
    }
}
