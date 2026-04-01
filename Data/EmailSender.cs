using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace SASTE.Data
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _settings;

        public EmailSender(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using var message = new MailMessage();
            message.From = new MailAddress(_settings.From, "SASTE");
            message.To.Add(email);
            message.Subject = subject;
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            using var client = new SmtpClient(_settings.SmtpServer, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }
    }
}