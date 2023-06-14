﻿using Csiro.Models;
using Csiro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csiro.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<ApplicationUser> userManager { get; }
		private SignInManager<ApplicationUser> signInManager { get; }

		public AccountController (UserManager<ApplicationUser> _userManager, 
			SignInManager<ApplicationUser> _signInManager)
		{
			this.userManager = _userManager;
			this.signInManager = _signInManager;
    
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel m)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = m.Email,
                    Email = m.Email,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                   /* Gpa = (float)m.Gpa,
                    CourseID = m.CourseID,
                    UniID = m.UniID,
                    CoverLetter = m.CoverLetter,
                    Resume = m.Resume*/
                };

                var result = await userManager.CreateAsync(user, m.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    await System.IO.File.WriteAllTextAsync("confirm.txt", confirmationLink.ToString());
                    ViewBag.ErrorTitle = "User creation is successful";

                    

                    return View("Error");
                }

                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError("", e.Description);
                }
            }

            return View(m);
        }
        public async Task <IActionResult> ConfirmEmail(string userId, string token) 
		{
			var user = await userManager.FindByIdAsync(userId);
			if(null ==user)return View("Error");
			
			var result = await userManager.ConfirmEmailAsync(user, token);

			if (result.Succeeded) return View("Login");

			return View("Error");

		}

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel m)
        {
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(m.Email, m.Password, m.RememberMe, false);

				if (result.Succeeded)
				{
					var user = await userManager.FindByNameAsync(m.Email);
					var userID = user.Id;


					//test

					HttpContext.Session.SetString("userId", userID);


					string strSession = HttpContext.Session.GetString("userId");

					//

					return RedirectToAction("Index", "Home");



				}
				ModelState.AddModelError("", "Invalid Attempt");
			
			}
                return View(new LoginViewModel());
        }

        public IActionResult Index()
		{
			return View();
		}

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}