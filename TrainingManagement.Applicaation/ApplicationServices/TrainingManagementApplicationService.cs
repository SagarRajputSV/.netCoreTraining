using TrainingManagement.Application.BusinessServices;

namespace TrainingManagement.Application.ApplicationServices
{    
    public abstract class TrainingManagementApplicationService<T> : ApplicationService<T>, ITrainingManagementApplicationService<T>
        where T : class
    {
        private readonly ITrainingManagementBusinessServices<T> _trainingManagementBusinessService;

        protected TrainingManagementApplicationService(ITrainingManagementBusinessServices<T> trainingManagementBusinessService)
            : base(trainingManagementBusinessService)
        {
            _trainingManagementBusinessService = trainingManagementBusinessService;
        }
    }
}
