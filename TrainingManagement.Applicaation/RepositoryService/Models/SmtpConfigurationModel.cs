using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class SmtpConfigurationModel
    {
        public bool EnableSsl { get; set; }
        public string NetworkUser { get; set; }
        public string NetworkPassword { get; set; }
        public string Smtp { get; set; }
        public int Port { get; set; }
        public string Environment { get; set; }
        public string DefaultToEmails { get; set; }
        public string DefaultCcEmails { get; set; }
        public string DefaultBcEmails { get; set; }
        public string SenderEmail { get; set; }
        public string LinkExpireTime { get; set; }
        public string SenderDisplayName { get; set; }
        public string SubjectPrefix { get; set; }
        public string EmailUploadSubject { get; set; }
    }
}
