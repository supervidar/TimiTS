using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models;
using TimiTS.Models.ViewModels;

namespace TimiTS.Controllers
{
    public class LoginController : Controller
    {
        private  UserManager<ApplicationUser> _userManager;
        private  RoleManager<ApplicationRole> _roleManager;
        private  SignInManager<ApplicationUser> _signInManager;

        public LoginController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(loginViewModel.UserName, 
                    loginViewModel.Password, loginViewModel.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Tømrer"))
                    {
                        
                        return RedirectToAction("Index", "Work", new { area = "Tømrer" });
                    }
                    if (roles.Contains("Administrasjon"))
                    {
                       
                        return RedirectToAction("Index", "Home", new { area = "Administrasjon" });
                    }                    
                }

                ModelState.AddModelError("", "Feil brukernavn eller passord");
                return View(loginViewModel);
            }

            return View(loginViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();            
            return RedirectToAction("Login");
        }

    }
}
