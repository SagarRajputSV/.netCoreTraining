using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class TraineeFeedbackApplicationServices : TrainingManagementApplicationService<TraineeFeedback>,
        ITraineeFeedbackApplicationServices
    {
        private readonly ITraineeFeedbackBusinessServices _traineeFeedbackBusinessServices;
        public TraineeFeedbackApplicationServices(ITraineeFeedbackBusinessServices traineeFeedbackBusinessServices)
            : base(traineeFeedbackBusinessServices)
        {
            _traineeFeedbackBusinessServices = traineeFeedbackBusinessServices;
        }

        public Task<bool> AddTraineeFeedback(TraineeFeedbackModel traineeFeedback, ClaimModel claimModel)
        {
            return _traineeFeedbackBusinessServices.AddTraineeFeedback(traineeFeedback, claimModel);
        }

        public Task<bool> ChaeckAlredyFeedbackSubmitted(Guid TraineerId, Guid TraineeId, Guid SubjectId, DateTime TrainingDate)
        {
           return _traineeFeedbackBusinessServices.ChaeckAlredyFeedbackSubmitted(TraineerId: TraineerId, TraineeId: TraineeId, SubjectId: SubjectId, TrainingDate: TrainingDate);
        }
    }
}
