using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class TraineerSubjectModel
    {
        public Guid Id { get; set; }
        public Guid TrainerId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Instructions { get; set; }
    }
}
