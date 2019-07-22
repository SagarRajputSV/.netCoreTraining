using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class ApplicationVersionBusinessServices : TrainingManagementBusinessService<ApplicationVersion>,
        IApplicationVersionBusinessServices
    {
        public ApplicationVersionBusinessServices(Func<ITrainingManagementUnitOfWork> iApplicationVersionBusinessServices) :
            base(iApplicationVersionBusinessServices)
        {

        }

        public async Task<ApplicationVersion> FindAsync(Guid id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<ApplicationVersion>().FindAsync(id);
            }
        }
    }
}
