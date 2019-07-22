using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class PreEnrollmentQuestionModel
    {
        public Guid TrainingId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int MinimumPassingMarks { get; set; }
        public int QuestionWeight { get; set; }
        public int MaxAnswerTime { get; set; }
        public string PrerequisiteLinks { get; set; }
        public bool IsActive { get; set; }
    }
}
