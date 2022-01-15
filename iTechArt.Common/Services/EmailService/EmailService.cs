using System.Threading.Tasks;
using iTechArt.iTechQuiz.Foundation.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace iTechArt.Common.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _options;


        public EmailService(IOptions<EmailServiceOptions> options)
        {
            _options = options.Value;
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_options.EmailName, _options.EmailAddress));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_options.SmtpHost, _options.SmtpHostPort, false);
            await client.AuthenticateAsync(_options.EmailAddress, _options.EmailPassword);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
