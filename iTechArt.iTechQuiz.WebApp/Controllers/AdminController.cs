using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Common.Extensions;
using iTechArt.Common.Lists;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories.Constants;
using iTechArt.iTechQuiz.WebApp.ViewModels;
using iTechArt.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;


        public AdminController(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Users()
        {
            var users = new List<UserViewModel>();
            foreach (var user in _userManager.Users.Where(u => !u.IsSystemUser).ToList())
            {
                users.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    CurrentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault().CapitalizeFirstLetter()
                });
            }

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (User.GetId() == id.ToString())
            {
                return RedirectToAction("Users");
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            var role = (await _userManager.GetFirstUserRoleAsync(user)).CapitalizeFirstLetter();

            return View(new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CurrentRole = role
            });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            if (User.GetId() == id.ToString())
            {
                return RedirectToAction("Users");
            }

            await _userManager.DeleteAsync(new User
            {
                Id = id
            });

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

            var userRole = await _userManager.GetFirstUserRoleAsync(user);
            return View(new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CurrentRole = userRole,
                Roles = await _roleManager.Roles.Select(p => p.Name).ToListAsync()
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

            var userRole = await _userManager.GetFirstUserRoleAsync(user);
            if (model.CurrentRole != userRole)
            {
                await _userManager.RemoveFromRoleAsync(user, userRole);
                await _userManager.AddToRoleAsync(user, model.CurrentRole);
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

            if (User.GetId() == user.Id.ToString())
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
                return View(model);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return View(model);
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Users");
        }
    }
}

