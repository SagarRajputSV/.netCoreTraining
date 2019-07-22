using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.Models;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;
using System.Linq;

namespace TrainingManagement.Application.BusinessServices
{
    public class PreEnrollmentUserAnswerBusinessServices : TrainingManagementBusinessService<PreEnrollmentUserAnswer>,
        IPreEnrollmentUserAnswerBusinessServices
    {
        public PreEnrollmentUserAnswerBusinessServices(Func<ITrainingManagementUnitOfWork> iPreEnrollmentQuestionBusinessServices) :
            base(iPreEnrollmentQuestionBusinessServices)
        {
        }

        public async Task<int> AddAll(ListOfAnswerModel listAnswers, ClaimModel claimModel)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                var repositoryServices = repositories.GetRepositoryServiceSqlServerEf<PreEnrollmentUserAnswer>();

                var requestObj = listAnswers.AnswerModels.Select(x => new PreEnrollmentUserAnswer
                {
                   QuestionId = x.QuestionId,
                   Comments = x.Comments,
                   Answer = x.Answer,
                   CourseEnrollmentId = x.CourseEnrollmentId,
                   CreatedBy = claimModel.UserId,
                   TimeTaken = x.TimeTaken,
                   CreatedDate = DateTime.Now
                }).ToList();

                repositoryServices.AddAll(requestObj);

                var result = await repositories.SaveChangesAsync();
                return result;
            }
        }

        public async Task<PreEnrollmentUserAnswer> FindAsync(Guid id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<PreEnrollmentUserAnswer>().FindAsync(id);
            }
        }
    }
}
