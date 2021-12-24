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
        public IActionResult Index()
        {
            return View();
        }
    }
}
