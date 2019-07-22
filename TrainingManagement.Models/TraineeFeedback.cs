using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    public class TraineeFeedback : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid TraineeId { get; set; }
        public Guid TrainerId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid QuestionId { get; set; }
        public decimal TraineeRating { get; set; }
        public string Comments { get; set; }
        public DateTime TrainingDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ApplicationUser  Trainee {get; set;}
        public virtual ApplicationUser  Trainer {get; set;}
        public virtual Subject Subject { get; set; }
        //public virtual Feedback Feedback { get; set; }
    }
}
