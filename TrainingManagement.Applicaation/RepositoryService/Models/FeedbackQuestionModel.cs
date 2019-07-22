using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class FeedbackQuestionModel
    {
        public string Question { get; set; }
        public int MaxMark { get; set; }
        public int MinMark { get; set; }
        public bool IsOptional { get; set; } = false;
        public bool IsActive { get; set; }
    }
}
