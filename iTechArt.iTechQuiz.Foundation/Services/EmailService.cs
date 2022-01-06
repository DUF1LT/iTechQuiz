using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace iTechArt.iTechQuiz.Foundation.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("iTechQuiz", "itechquiz@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.yandex.ru", 25, true);
            await client.AuthenticateAsync("itechquiz@yandex.ru", "itechquiz2021");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
