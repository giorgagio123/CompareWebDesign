using CompareWebDesign.Domain;
using CompareWebDesign.Models.Account;
using CompareWebDesign.Services.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Register() => View(new RegisterModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var userExist = await _accountService.FindByNameAsync(model.UserName);
            if (userExist != null)
            {
                ModelState.AddModelError("", "User Already Exists");
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await _accountService.CreateUserAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login(string returnUrl = "") => View(new LoginModel() { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    var result = await _accountService.PasswordSignInAsync(model.UserName, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("ProjectList", "Home");
                        }
                    }
                }
            }

            ModelState.AddModelError("", "Invalid login attempt");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();

            return RedirectToAction("ProjectList", "Home");
        }
    }
}
