using System.Threading.Tasks;

namespace iTechArt.Common.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
