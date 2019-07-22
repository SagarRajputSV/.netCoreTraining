using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class TraineeFeedbackModel
    {
        public string TraineeId { get; set; }
        public string TraineerId { get; set; }
        public string SubjectId { get; set; }
        public DateTime TrainingDate { get; set; }
        public List<QuestionModel> QuestionModels { get; set; }
    }

    public class QuestionModel
    {
        public string QuestionId { get; set; }
        public decimal TraineeRating { get; set; }
        public string Comments { get; set; }
    }
}
