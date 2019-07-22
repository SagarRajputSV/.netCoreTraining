using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class SendVerificationMailModel
    {
        public string VerificationId { get; set; }
        public string ReturnUrl { get; set; }
        public string Key { get; set; }
    }
}
