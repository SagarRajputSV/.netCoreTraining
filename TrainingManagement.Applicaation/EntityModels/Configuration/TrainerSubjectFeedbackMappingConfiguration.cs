using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels.Configuration
{
    public class TrainerSubjectFeedbackMappingConfiguration : IEntityTypeConfiguration<TrainerSubjectFeedbackMapping>
    {
        public void Configure(EntityTypeBuilder<TrainerSubjectFeedbackMapping> builder)
        {
            builder.ToTable("TrainerSubjectFeedbackMapping").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.FeedbackQuestionId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.TrainerSubjectId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.ObtainMarks).HasColumnType("int").IsRequired();
            builder.Property(x => x.Description).HasColumnType("nvarchar(500)").IsRequired().HasMaxLength(500);

            builder.HasOne(f => f.FeedbackQuestions).WithMany(t => t.TrainerSubjectFeedbackMappings)
                .HasForeignKey(f => f.FeedbackQuestionId);

            builder.HasOne(c => c.TrainerSubjectMapping).WithMany(l => l.TrainerSubjectFeedbackMappings).HasForeignKey(c => c.TrainerSubjectId);
        }
    }
}
