using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models;

namespace TimiTS.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private int wId;

        public HomeController(UserManager<ApplicationUser> usermanager)
        {          
            _userManager = usermanager;
           
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Check if the user is logged in or not. Sends the user to its area based on role.
            var user = await GetCurrentUserAsync();
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {               
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Tømrer"))
                {
                    return RedirectToAction("Index", "Work", new { area = "Tømrer" });
                }
                if (roles.Contains("Administrasjon"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Administrasjon" });
                }
                return View();
            }
            else
            {
                return View();
            }
                      
        }

        #region Helpers
        private Task<ApplicationUser> GetCurrentUserAsync()
        {

            return _userManager.GetUserAsync(HttpContext.User);

        }
       
        #endregion
    }

}
