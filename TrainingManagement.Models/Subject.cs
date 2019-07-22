using System;
using System.Collections.Generic;

namespace TrainingManagement.Models
{
    public class Subject : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Prerequisites { get; set; }
        public decimal Price { get; set; }
        public bool IsFree { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<TrainerSubjectMapping> TrainerSubjectMappings { get; set; }
        public virtual ICollection<TraineeFeedback> TraineeFeedbacks { get; set; }
    }
}
