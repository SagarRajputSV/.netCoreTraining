using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Application.EntityModels.Configuration;
using TrainingManagement.Models;

namespace TrainingManagement.Application.EntityModels
{
    public sealed class TrainingManagementDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public TrainingManagementDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TrainerSubjectMapping> TrainerSubjectMappings { get; set; }
        public DbSet<TrainerSubjectFeedbackMapping> TrainerSubjectFeedbackMappings { get; set; }
        public DbSet<TopHeaderInformation> TopHeaderInformations { get; set; }
        public DbSet<TraineeFeedback> TraineeFeedbacks { get; set; }
        public DbSet<PreEnrollmentQuestions> PreEnrollmentQuestions { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<ApplicationVersion> ApplicationVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbackQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new TrainerConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new TrainerSubjectMappingConfiguration());
            modelBuilder.ApplyConfiguration(new TrainerSubjectFeedbackMappingConfiguration());
            modelBuilder.ApplyConfiguration(new TopHeaderInformationConfiguration());
            //modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
            modelBuilder.ApplyConfiguration(new TraineeFeedbackConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new PreEnrollmentQuestionsConfiguration());
            modelBuilder.ApplyConfiguration(new CourseEnrollmentConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicarionVersionConfiguration());
        }
    }

    public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<TrainingManagementDbContext>
    {
        TrainingManagementDbContext IDesignTimeDbContextFactory<TrainingManagementDbContext>.CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TrainingManagementDbContext> optionsBuilder = new DbContextOptionsBuilder<TrainingManagementDbContext>();
            if (!optionsBuilder.IsConfigured)
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new[] { @"bin\" }, StringSplitOptions.None)[0];
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(projectPath).AddJsonFile("appsettings.json").Build();
                string connectionString = configuration.GetConnectionString("TrainingManagementConnection");
                optionsBuilder.EnableSensitiveDataLogging().UseLazyLoadingProxies().UseSqlServer(connectionString);
            }

            return new TrainingManagementDbContext(optionsBuilder.Options);
        }
    }
}
