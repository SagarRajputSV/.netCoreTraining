using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Application.Models;

namespace TrainingManagement.Application.EmailHelper
{
    public class EmailHelper
    {
        private readonly SmtpConfigurationModel _smtpConfigurationModel;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;

        public EmailHelper(SmtpConfigurationModel smtpConfigurationModel, IHostingEnvironment hostingEnvironment, ILogger logger)
        {
            _smtpConfigurationModel = smtpConfigurationModel;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string email, string displayName, string subject, string emailBody, string ccEmails)
        {
            bool mailStatus = false;
            try
            {
                List<Recipient> recipients = new List<Recipient>
                {
                    new Recipient()
                    {
                        DisplayName = displayName,
                        Email = email
                    }
                };

                MailMessage mailMessage = new MailMessage
                {
                    Body = emailBody,
                    SenderEmail = _smtpConfigurationModel.SenderEmail,
                    Subject = _smtpConfigurationModel.SubjectPrefix + subject,
                    SenderDisplayName = _smtpConfigurationModel.SenderDisplayName,
                    Recipients = recipients
                };

                SmtpConfigurationModel smtpConfig = new SmtpConfigurationModel
                {
                    Environment = _smtpConfigurationModel.Environment,
                    EnableSsl = _smtpConfigurationModel.EnableSsl,
                    DefaultBcEmails = _smtpConfigurationModel.DefaultBcEmails,
                    DefaultCcEmails = _smtpConfigurationModel.DefaultCcEmails,
                    DefaultToEmails = _smtpConfigurationModel.DefaultToEmails,
                    NetworkPassword = _smtpConfigurationModel.NetworkPassword,
                    NetworkUser = _smtpConfigurationModel.NetworkUser,
                    Port = _smtpConfigurationModel.Port,
                    Smtp = _smtpConfigurationModel.Smtp
                };

                if (!_smtpConfigurationModel.Environment.Equals("Dev", StringComparison.InvariantCultureIgnoreCase))
                {
                    smtpConfig.DefaultToEmails = email;
                    smtpConfig.DefaultCcEmails = ccEmails;
                }

                mailStatus = await MailServices.SendMailAsync(mailMessage, smtpConfig, _hostingEnvironment.WebRootPath + @"\images\CognizantSoftvision-logo-black.png");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unable to send email");
            }

            return mailStatus;
        }
    }
}
