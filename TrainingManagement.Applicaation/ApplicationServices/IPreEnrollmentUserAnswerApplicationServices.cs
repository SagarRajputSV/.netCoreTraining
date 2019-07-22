using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public interface IPreEnrollmentUserAnswerApplicationServices:ITrainingManagementApplicationService<PreEnrollmentUserAnswer>
    {
        Task<int> AddAll(ListOfAnswerModel listAnswers, ClaimModel claimModel);
    }
}
