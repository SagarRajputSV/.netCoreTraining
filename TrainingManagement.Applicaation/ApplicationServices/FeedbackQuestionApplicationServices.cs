using System;
using System.Collections.Generic;
using System.Text;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class FeedbackQuestionApplicationServices : TrainingManagementApplicationService<FeedbackQuestion>,
        IFeedbackQuestionApplicationServices
    {
        private readonly IFeedbackQuestionBusinessServices _feedbackQuestionBusinessServices;
        public FeedbackQuestionApplicationServices(IFeedbackQuestionBusinessServices feedbackQuestionBusinessServices)
            : base(feedbackQuestionBusinessServices)
        {
            _feedbackQuestionBusinessServices = feedbackQuestionBusinessServices;
        }
    }
}
