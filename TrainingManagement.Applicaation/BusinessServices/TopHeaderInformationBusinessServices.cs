using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class TopHeaderInformationBusinessServices: TrainingManagementBusinessService<TopHeaderInformation>,ITopHeaderInformationBusinessServices
    {
        public TopHeaderInformationBusinessServices(Func<ITrainingManagementUnitOfWork> trainingManagementRepositoryService) : base(trainingManagementRepositoryService)
        {
        }
    }
}
