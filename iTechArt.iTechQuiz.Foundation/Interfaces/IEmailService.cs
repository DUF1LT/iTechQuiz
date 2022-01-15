using System.Threading.Tasks;

namespace iTechArt.iTechQuiz.Foundation.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
