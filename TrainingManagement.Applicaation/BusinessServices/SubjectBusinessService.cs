using System;
using System.Threading.Tasks;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class SubjectBusinessService : TrainingManagementBusinessService<Subject>, ISubjectBusinessService
    {
        public SubjectBusinessService(Func<ITrainingManagementUnitOfWork> trainingManagementRepositoryService) : base(trainingManagementRepositoryService)
        {
        }

        public async Task<Subject>FindAsync(Guid id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<Subject>().FindAsync(id);
            }
        }
    }
}
