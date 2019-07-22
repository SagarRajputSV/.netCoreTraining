using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public interface IPreEnrollmentUserAnswerBusinessServices : ITrainingManagementBusinessServices<PreEnrollmentUserAnswer>
    {
        Task<PreEnrollmentUserAnswer> FindAsync(Guid id);
        Task<int> AddAll(ListOfAnswerModel listAnswers, ClaimModel claimModel);
    }
}
