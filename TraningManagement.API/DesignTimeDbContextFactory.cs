using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrainingManagement.Application.EntityModels;

namespace TraningManagement.API
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TrainingManagementDbContext>
    {
        public TrainingManagementDbContext CreateDbContext(string[] args)
        {
            //Mapper.Reset();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<TrainingManagementDbContext>();

            builder.UseSqlServer(configuration["ConnectionStrings:TrainingManagementConnection"], b => b.MigrationsAssembly("TrainingManagement.Application"));
            //builder.UseOpenIddict();

            return new TrainingManagementDbContext(builder.Options);
        }
    }
}
