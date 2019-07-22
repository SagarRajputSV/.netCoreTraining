using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class TrainerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Experience { get; set; }
        public Guid TrainerImage { get; set; }
        public string Skills { get; set; }
        public string AboutTrainer { get; set; }
        public string FacebookId { get; set; }
        public string InstagramId { get; set; }
        public string TwitterId { get; set; }
        public bool IsActive { get; set; }
    }
}
