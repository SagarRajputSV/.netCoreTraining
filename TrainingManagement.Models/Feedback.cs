using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    /// <summary>
    /// This Table will be use to store all kind of feedback from both Trainer and Trainee
    /// </summary>
    public class Feedback:BaseEntity
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public decimal TraineeRating { get; set; }
        public string TraineeComments { get; set; }
        public decimal TrainerRating { get; set; }
        public string TrainerComments { get; set; }

        public virtual FeedbackQuestion FeedbackQuestion { get; set; }
        //public virtual ICollection<TraineeFeedback> TraineeFeedbacks { get; set; }
    }
}
