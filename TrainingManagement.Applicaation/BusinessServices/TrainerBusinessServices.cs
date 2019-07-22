using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class TrainerBusinessServices : TrainingManagementBusinessService<Trainer>, 
        ITrainerBusinessServices
    {
        public TrainerBusinessServices(Func<ITrainingManagementUnitOfWork> trainingManagementRepositoryService) : base(trainingManagementRepositoryService)
        {
        }

        public async Task<Trainer> FindAsync(Guid id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<Trainer>().FindAsync(id);
            }
        }
    }
}
