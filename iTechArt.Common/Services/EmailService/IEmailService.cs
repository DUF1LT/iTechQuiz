using System.Threading.Tasks;

<<<<<<< HEAD:iTechArt.Common/Services/EmailService/IEmailService.cs
namespace iTechArt.Common.Services.EmailService
=======
namespace iTechArt.iTechQuiz.Foundation.Interfaces
>>>>>>> a869f68 (create user service, modify unitOfWork, create specific repositories, add custom role and userrole):iTechArt.iTechQuiz.Foundation/Interfaces/IEmailService.cs
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
