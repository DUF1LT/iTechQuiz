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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public AdminController(UserManager<IdentityUser<Guid>> userManager, SignInManager<IdentityUser<Guid>> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

            var role = (await _userManager.GetFirstUserRoleAsync(user))
                .CapitalizeFirstLetter();

            return View(new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = role
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

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            bool isAdmin = await _userManager.UserIsAdminAsync(user, Roles.Admin);

            return View(new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsAdmin = isAdmin
            });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == model.Id);
            user.UserName = model.UserName;
            user.Email = model.Email;

            var isUserAdmin = await _userManager.UserIsAdminAsync(user, Roles.Admin);
            if (model.IsAdmin && !isUserAdmin)
            {
                await _userManager.RemoveFromRoleAsync(user, Roles.User);
                await _userManager.AddToRoleAsync(user, Roles.Admin);
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, Roles.Admin);
                await _userManager.AddToRoleAsync(user, Roles.User);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            if (User.Identity.Name.Equals(user.UserName))
            {
               await _signInManager.SignInAsync(user, false);

               return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Users");
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> EditPassword(Guid id)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            return View(new EditPasswordViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
            });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> EditPassword(EditPasswordViewModel model)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == model.Id);

            if (user is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(new EditPasswordViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                });
            }

            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return View(new EditPasswordViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                });
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Users");
        }
    }
}

