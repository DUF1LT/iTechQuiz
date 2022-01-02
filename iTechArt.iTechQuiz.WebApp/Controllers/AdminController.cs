using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Common.Extensions;
using iTechArt.iTechQuiz.Repositories.Constants;
using iTechArt.iTechQuiz.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;


        public AdminController(UserManager<IdentityUser<Guid>> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Users()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (var user in _userManager.Users.ToList())
            {
                users.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault().CapitalizeFirstLetter()
                });
            }

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return NotFound();
            }

            return View(new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault().CapitalizeFirstLetter()
            });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Users");
        }

    }
}
