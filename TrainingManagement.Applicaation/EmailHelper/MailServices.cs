using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.Models;
using MailMessage = TrainingManagement.Application.Models.MailMessage;

namespace TrainingManagement.Application.EmailHelper
{
    public class MailServices
    {
        public static async Task<bool> SendMailAsync(MailMessage mail, SmtpConfigurationModel smtpConfigurationModel, string logoPath)
        {
            if (mail == null) return false;
            try
            {
                using (SmtpClient smtpClient = ConfigureSmtp(smtpConfigurationModel))
                {
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage
                    {
                        From = new MailAddress(mail.SenderEmail),
                        Subject = mail.Subject,
                        IsBodyHtml = true,
                    };

                    mailMessage.AlternateViews.Add(GetMailBody(logoPath, mail.Body));

                    if (smtpConfigurationModel.Environment.Equals("Production", StringComparison.InvariantCultureIgnoreCase) ||
                        smtpConfigurationModel.Environment.Equals("SIT", StringComparison.InvariantCultureIgnoreCase))
                    {
                        mail.Recipients?.ForEach(x => mailMessage.To.Add(new MailAddress(x.Email, x.DisplayName)));
                        mail.RecipientsCc?.ForEach(x => mailMessage.CC.Add(new MailAddress(x.Email, x.DisplayName)));
                    }
                    else
                    {
                        List<string> toList = smtpConfigurationModel.DefaultToEmails?.Split(',').ToList();
                        List<string> ccList = smtpConfigurationModel.DefaultCcEmails?.Split(',').ToList();
                        toList?.ForEach(x => mailMessage.To.Add(new MailAddress(x, x)));
                        ccList?.ForEach(x => mailMessage.CC.Add(new MailAddress(x, x)));
                    }

                    if (mail.Attachments != null && mail.Attachments.Count > 0)
                    {
                        mail.Attachments.ForEach(x => mailMessage.Attachments.
                            Add(new System.Net.Mail.Attachment(new MemoryStream(x.AttachmentBytes), x.FileName,
                                x.ContentType)));
                    }
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        private static SmtpClient ConfigureSmtp(SmtpConfigurationModel smtpConfigurationModel)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(smtpConfigurationModel.Smtp, smtpConfigurationModel.Port)
                {
                    EnableSsl = smtpConfigurationModel.EnableSsl
                };

                if (string.IsNullOrWhiteSpace(smtpConfigurationModel.NetworkUser) &&
                    string.IsNullOrWhiteSpace(smtpConfigurationModel.NetworkPassword))
                {
                    smtpClient.UseDefaultCredentials = false;
                }
                else
                {
                    smtpClient.Credentials = new NetworkCredential(smtpConfigurationModel.NetworkUser, smtpConfigurationModel.NetworkPassword);
                }

                return smtpClient;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static AlternateView GetMailBody(string logoPath, string body)
        {
            LinkedResource linkedResource = new LinkedResource(logoPath, MediaTypeNames.Image.Jpeg);
            linkedResource.ContentId = Guid.NewGuid().ToString();
            body = body.Replace("$logo", linkedResource.ContentId);
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(linkedResource);

            return alternateView;
        }
    }
}
