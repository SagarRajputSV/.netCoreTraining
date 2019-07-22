using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class ImagesBusinessService : TrainingManagementBusinessService<Images>, IImagesBusinessService
    {
        public ImagesBusinessService(Func<ITrainingManagementUnitOfWork> trainingManagementRepositoryService) : base(trainingManagementRepositoryService)
        {
        }

        public async Task<Images> FindAsync(Guid id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<Images>().FindAsync(id);
            }
        }
    }
}
