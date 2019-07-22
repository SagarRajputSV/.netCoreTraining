using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class FeedbackQuestionBusinessServices : TrainingManagementBusinessService<FeedbackQuestion>, 
        IFeedbackQuestionBusinessServices
    {
        public FeedbackQuestionBusinessServices(Func<ITrainingManagementUnitOfWork> feedbackQuestionBusinessServices) :
            base(feedbackQuestionBusinessServices)
        {
        }
    }
}
