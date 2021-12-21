using System;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public AboutController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //GET: Index
        public async Task<IActionResult> Index()
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            user.UserName = "admin";
            user.Email = "admin@itechart.com";
            user.PasswordHash = "admin".GetHashCode().ToString();

            await _unitOfWork.GetRepository<User>().CreateAsync(user);
            await _unitOfWork.SaveAsync();

            return View();
        }
    }
}
