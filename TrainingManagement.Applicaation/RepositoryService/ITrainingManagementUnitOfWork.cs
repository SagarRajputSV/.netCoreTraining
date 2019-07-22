using TraningManagement.Infrastructure.RepositoryService;

namespace TrainingManagement.Application.RepositoryService
{
    public interface ITrainingManagementUnitOfWork : IUnitOfWork
    {
        ISqlServerEfRepositoryService<T> GetSqlServerEfRepositoryService<T>() where T : class;
    }
}
