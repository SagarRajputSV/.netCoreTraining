using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class ClaimModel
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
    }
}
