using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class PreEnrollmentUserAnswerApplicationServices : TrainingManagementApplicationService<PreEnrollmentUserAnswer>,
        IPreEnrollmentUserAnswerApplicationServices
    {
        private readonly IPreEnrollmentUserAnswerBusinessServices _preEnrollmentUserAnswerBusinessServices;
        public PreEnrollmentUserAnswerApplicationServices(IPreEnrollmentUserAnswerBusinessServices preEnrollmentUserAnswerBusinessServices)
            : base(preEnrollmentUserAnswerBusinessServices)
        {
            _preEnrollmentUserAnswerBusinessServices = preEnrollmentUserAnswerBusinessServices;
        }

        public async Task<PreEnrollmentUserAnswer> FindAsync(Guid id)
        {
            return await _preEnrollmentUserAnswerBusinessServices.FindAsync(id);
        }

        public async Task<int> AddAll(ListOfAnswerModel listAnswers, ClaimModel claimModel)
        {
            return await _preEnrollmentUserAnswerBusinessServices.AddAll(listAnswers, claimModel);
        }
    }
}
