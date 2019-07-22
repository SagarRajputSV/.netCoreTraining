using System.Collections.Generic;

namespace TrainingManagement.Application.Models
{
    public class MailMessage
    {
        public string SenderEmail { get; set; }
        public string SenderDisplayName { get; set; }
        public List<Recipient> Recipients { get; set; }
        public List<Recipient> RecipientsCc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }
    }

    public class Recipient
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
    public class Attachment
    {
        public byte[] AttachmentBytes { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
