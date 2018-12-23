using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TWork.Models.Entities;
using TWork.Models.ModelValidators;
using TWork.Models.Services;
using TWork.Models.ViewModels;

namespace TWork.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IRegisterUserModelValidator _registerUserValidator;

        public AccountController(IUserService userService, IRegisterUserModelValidator registerUserValidator)
        {
            _userService = userService;
            _registerUserValidator = registerUserValidator;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool succeeded = await _userService.LoginAsync(details);
                if(succeeded)
                    return Redirect(returnUrl ?? "/");                
            }
            ModelState.AddModelError(nameof(LoginUserModel.Email), "Invalid user or password");

            return View(details);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (!(await _registerUserValidator.ValidateRegisterUserModel(model, ModelState)))
                {
                    IdentityResult result = await _userService.CreateUserAsync(model);
                    if (result.Succeeded)
                        return RedirectToAction("Login");
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.SingOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}