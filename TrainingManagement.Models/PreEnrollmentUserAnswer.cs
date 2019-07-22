using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    public class PreEnrollmentUserAnswer:BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CourseEnrollmentId { get; set; }
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public string Comments { get; set; }
        public string TimeTaken { get; set; }

        public virtual CourseEnrollment CourseEnrollment { get; set; }
    }
}