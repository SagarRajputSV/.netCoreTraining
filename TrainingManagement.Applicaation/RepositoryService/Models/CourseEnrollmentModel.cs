using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class CourseEnrollmentModel
    {
        public Guid Id { get; set; }    
        public Guid TrainingId { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsPreEnrollmentTestCompleted { get; set; } = false;
        public string PreEnrollmentUrl { get; set; }
    }
}
