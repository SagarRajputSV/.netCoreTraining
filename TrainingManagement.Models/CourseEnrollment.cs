using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    public class CourseEnrollment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid TrainingId { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsPreEnrollmentLinkVisited { get; set; } = false;

        public virtual TrainerSubjectMapping TrainerSubjectMappings { get; set; }
        public virtual ICollection<PreEnrollmentUserAnswer> PreEnrollmentUserAnswers { get; set; }
    }
}
