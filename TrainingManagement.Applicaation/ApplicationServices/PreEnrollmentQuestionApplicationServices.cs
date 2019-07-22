using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class PreEnrollmentQuestionApplicationServices : TrainingManagementApplicationService<PreEnrollmentQuestions>,
        IPreEnrollmentQuestionApplicationServices
    {
        private readonly IPreEnrollmentQuestionBusinessServices _preEnrollmentQuestionBusinessServices;
        public PreEnrollmentQuestionApplicationServices(IPreEnrollmentQuestionBusinessServices preEnrollmentQuestionBusinessServices)
            : base(preEnrollmentQuestionBusinessServices)
        {
            _preEnrollmentQuestionBusinessServices = preEnrollmentQuestionBusinessServices;
        }

        public async Task<PreEnrollmentQuestions> FindAsync(Guid id)
        {
            return await _preEnrollmentQuestionBusinessServices.FindAsync(id);
        }
    }
}
