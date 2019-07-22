using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class TopHeaderInformationConfiguration : IEntityTypeConfiguration<TopHeaderInformation>
    {
        public void Configure(EntityTypeBuilder<TopHeaderInformation> builder)
        {
            builder.ToTable("TopHeaderInformation").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.OfficialContactEmail).HasColumnName("OfficialContactEmail").HasColumnType("nvarchar(100)").IsRequired().HasMaxLength(100);
            builder.Property(x => x.OfficialContactNumber).HasColumnName("OfficialContactNumber").HasColumnType("nvarchar(100)").IsRequired().HasMaxLength(100);
            builder.Property(x => x.OfficialFacebookId).HasColumnName("OfficialFacebookId").HasColumnType("nvarchar(100)").IsRequired().HasMaxLength(100);
            builder.Property(x => x.OfficialInstagramId).HasColumnName("OfficialInstagramId").HasColumnType("nvarchar(100)").IsRequired().HasMaxLength(100);
            builder.Property(x => x.OfficialTwitterId).HasColumnName("OfficialTwitterId").HasColumnType("nvarchar(100)").IsRequired().HasMaxLength(100);
            builder.Property(x => x.OfficialLinkedInId).HasColumnName("OfficialLinkedInId").HasColumnType("nvarchar(100)").IsRequired().HasMaxLength(100);
            builder.Property(x => x.IsActive).HasColumnType("bit");
        }
    }
}
