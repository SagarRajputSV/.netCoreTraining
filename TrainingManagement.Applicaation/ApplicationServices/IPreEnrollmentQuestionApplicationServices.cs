using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public interface IPreEnrollmentQuestionApplicationServices: ITrainingManagementApplicationService<PreEnrollmentQuestions>
    {
        Task<PreEnrollmentQuestions> FindAsync(Guid id);
    }
}
