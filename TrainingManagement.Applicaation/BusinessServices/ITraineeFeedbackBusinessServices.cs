using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public interface ITraineeFeedbackBusinessServices : ITrainingManagementBusinessServices<TraineeFeedback>
    {
        Task<bool> AddTraineeFeedback(TraineeFeedbackModel traineeFeedback, ClaimModel claimModel);
        Task<bool> ChaeckAlredyFeedbackSubmitted(Guid TraineerId, Guid TraineeId, Guid SubjectId, DateTime TrainingDate);
    }
}
