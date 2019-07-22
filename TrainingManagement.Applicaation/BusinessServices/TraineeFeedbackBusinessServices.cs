using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.Models;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class TraineeFeedbackBusinessServices : TrainingManagementBusinessService<TraineeFeedback>,
        ITraineeFeedbackBusinessServices
    {
        public TraineeFeedbackBusinessServices(Func<ITrainingManagementUnitOfWork> traineeFeedbackBusinessServices) :
            base(traineeFeedbackBusinessServices)
        {
        }

        public async Task<bool> ChaeckAlredyFeedbackSubmitted(Guid TraineerId, Guid TraineeId, Guid SubjectId, DateTime TrainingDate)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                var repositoryService = await repositories.GetRepositoryServiceSqlServerEf<TraineeFeedback>()
                    .GetAsync(x => x.TraineeId == TraineeId && x.TrainerId == TraineerId && x.SubjectId == SubjectId && x.TrainingDate == TrainingDate);

                return repositoryService.Count > 0 ? false : true;
            }
        }
        public async Task<bool> AddTraineeFeedback(TraineeFeedbackModel traineeFeedback, ClaimModel claimModel)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                var repositoryService = repositories.GetRepositoryServiceSqlServerEf<TraineeFeedback>();
                var traineeFeedbackObj = traineeFeedback.QuestionModels.Select(x => new TraineeFeedback
                {
                    QuestionId = Guid.Parse(x.QuestionId),
                    SubjectId = Guid.Parse(traineeFeedback.SubjectId),
                    TraineeId = Guid.Parse(traineeFeedback.TraineeId),
                    TrainerId = Guid.Parse(traineeFeedback.TraineerId),
                    TraineeRating = x.TraineeRating,
                    IsActive = true,
                    TrainingDate = traineeFeedback.TrainingDate,
                    Comments = x.Comments,
                    CreatedBy = traineeFeedback.TraineeId,//claimModel.UserId,
                    CreatedDate = DateTime.Now
                }).ToList();
                repositoryService.AddAll(traineeFeedbackObj);
                var result = await repositories.SaveChangesAsync() > 0;
                return result;
            }

        }
    }
}
