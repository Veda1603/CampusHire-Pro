using CampusHire.API.Models;
using CampusHire.API.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CampusHire.API.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress(
                _settings.SenderName,
                _settings.SenderEmail));

            mail.To.Add(MailboxAddress.Parse(email));

            mail.Subject = subject;

            mail.Body = new TextPart("html")
            {
                Text = message
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _settings.SmtpServer,
                _settings.Port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _settings.SenderEmail,
                _settings.Password);

            await smtp.SendAsync(mail);

            await smtp.DisconnectAsync(true);
        }
    }
}