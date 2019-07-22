using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public interface ITraineeFeedbackApplicationServices : ITrainingManagementApplicationService<TraineeFeedback>
    {
        Task<bool> AddTraineeFeedback(TraineeFeedbackModel traineeFeedback, ClaimModel claimModel);
        Task<bool> ChaeckAlredyFeedbackSubmitted(Guid TraineerId, Guid TraineeId, Guid SubjectId, DateTime TrainingDate);
    }
}
