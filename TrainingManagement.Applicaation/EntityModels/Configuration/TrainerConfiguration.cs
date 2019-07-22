using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.ToTable("Trainer").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Name).HasColumnType("nvarchar(250)").IsRequired().HasMaxLength(250);
            builder.Property(x => x.Experience).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.TrainerImage).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
            builder.Property(x => x.Skills).HasColumnType("nvarchar(250)").IsRequired().HasMaxLength(250);
            builder.Property(x => x.AboutTrainer).HasColumnType("nvarchar(250)").IsRequired().HasMaxLength(250);
        }
    }
}
