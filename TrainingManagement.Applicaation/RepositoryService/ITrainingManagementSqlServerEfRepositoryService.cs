using System;
using System.Collections.Generic;
using System.Text;
using TraningManagement.Infrastructure.RepositoryService;

namespace TrainingManagement.Application.RepositoryService
{
    public interface ITrainingManagementSqlServerEfRepositoryService<T> : IRepositoryService<T>
        where T : class
    {
    }
}
