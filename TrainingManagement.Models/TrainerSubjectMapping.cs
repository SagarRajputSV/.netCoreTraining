using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    public class TrainerSubjectMapping : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid TrainerId { get; set; }
        public Guid SubjectId { get; set; }
        public string Instructions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Trainer Trainer { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<TrainerSubjectFeedbackMapping> TrainerSubjectFeedbackMappings { get; set; }
        public virtual ICollection<PreEnrollmentQuestions> PreEnrollmentQuestions { get; set; }
        public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; }
    }
}
