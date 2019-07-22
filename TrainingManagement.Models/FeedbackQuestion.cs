using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    public class FeedbackQuestion:BaseEntity
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public int MaxMark { get; set; }
        public int MinMark { get; set; }
        public bool IsOptional { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TrainerSubjectFeedbackMapping> TrainerSubjectFeedbackMappings { get; set; }
    }
}
