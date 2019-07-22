using System;

namespace TrainingManagement.Models
{
    public class TrainerSubjectFeedbackMapping : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid TrainerSubjectId { get; set; }
        public Guid FeedbackQuestionId { get; set; }
        public int ObtainMarks { get; set; }
        public string Description { get; set; }
        public virtual TrainerSubjectMapping TrainerSubjectMapping { get; set; }
        public virtual FeedbackQuestion FeedbackQuestions { get; set; }
    }
}
