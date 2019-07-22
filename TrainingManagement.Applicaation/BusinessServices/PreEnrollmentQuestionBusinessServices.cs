using System;
using System.Threading.Tasks;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class PreEnrollmentQuestionBusinessServices : TrainingManagementBusinessService<PreEnrollmentQuestions>,
        IPreEnrollmentQuestionBusinessServices
    {
        public PreEnrollmentQuestionBusinessServices(Func<ITrainingManagementUnitOfWork> preEnrollmentQuestionsBusinessServices) :
            base(preEnrollmentQuestionsBusinessServices)
        {
        }

        public async Task<PreEnrollmentQuestions> FindAsync(Guid id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<PreEnrollmentQuestions>().FindAsync(id);
            }
        }
    }
}
