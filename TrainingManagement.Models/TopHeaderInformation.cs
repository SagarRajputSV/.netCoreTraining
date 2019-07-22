using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    public class TopHeaderInformation:BaseEntity
    {
        public Guid Id { get; set; }
        public string OfficialContactNumber { get; set; }
        public string OfficialContactEmail { get; set; }
        public string OfficialFacebookId { get; set; }
        public string OfficialInstagramId { get; set; }
        public string OfficialTwitterId { get; set; }
        public string OfficialLinkedInId { get; set; }
        public bool IsActive { get; set; }
    }
}
