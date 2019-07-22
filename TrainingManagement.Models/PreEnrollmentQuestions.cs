using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    public class PreEnrollmentQuestions:BaseEntity
    {
        public Guid Id { get; set; }
        public Guid TrainingId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int MinimumPassingMarks { get; set; }
        public int QuestionWeight { get; set; }
        public string PrerequisiteLinks { get; set; }
        public int MaxAnswerTime { get; set; }
        public bool IsActive { get; set; }

        public virtual TrainerSubjectMapping TrainerSubjectMapping { get; set; }
    }
}
