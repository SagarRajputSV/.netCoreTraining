using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class TopHeaderInformationModel
    {
        public Guid Id { get; set; }
        [Required (AllowEmptyStrings = false)]
        public string OfficialContactNumber { get; set; }
        [Required (AllowEmptyStrings = false)]
        public string OfficialContactEmail { get; set; }
        [Required (AllowEmptyStrings = false)]
        public string OfficialFacebookId { get; set; }
        [Required (AllowEmptyStrings = false)]
        public string OfficialInstagramId { get; set; }
        [Required (AllowEmptyStrings = false)]
        public string OfficialTwitterId { get; set; }
        [Required (AllowEmptyStrings = false)]
        public string OfficialLinkedInId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
