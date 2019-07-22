using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class TaineerSubjectBusinessServices : TrainingManagementBusinessService<TrainerSubjectMapping>, ITaineerSubjectBusinessServices
    {
        public TaineerSubjectBusinessServices(Func<ITrainingManagementUnitOfWork> trainingManagementRepositoryService) : base(trainingManagementRepositoryService)
        {
        }

        public async Task<TrainerSubjectMapping> FindAsync(Guid id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<TrainerSubjectMapping>().FindAsync(id);
            }
        }
    }
}
