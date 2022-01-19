using System.Threading.Tasks;

namespace iTechArt.iTechQuiz.Foundation.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
