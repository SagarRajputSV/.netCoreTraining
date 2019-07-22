using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Application.RepositoryService;

namespace TrainingManagement.Application.BusinessServices
{
    public class TrainingManagementBusinessService<T> : BusinessServiceBase<T>, ITrainingManagementBusinessServices<T>
        where T : class
    {
        public TrainingManagementBusinessService(Func<ITrainingManagementUnitOfWork> trainingManagementRepositoryService)
            : base(trainingManagementRepositoryService)
        {
        }
    }
}
