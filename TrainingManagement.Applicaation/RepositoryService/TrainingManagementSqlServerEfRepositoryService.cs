using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TraningManagement.Infrastructure.RepositoryService;

namespace TrainingManagement.Application.RepositoryService
{
    public class TrainingManagementSqlServerEfRepositoryService<T> : SqlServerEfRepositoryService<T>, ITrainingManagementSqlServerEfRepositoryService<T>
        where T : class
    {
        public TrainingManagementSqlServerEfRepositoryService(DbContext context) : base(context)
        {
        }
    }
}
