using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models;
using TimiTS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
/// <summary>
/// </summary>
namespace TimiTS.Areas.Tømrer.Controllers
{
     
    public class ManageController : DomainController
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public ManageController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = _userManager.Users.Where(u => u.UserName == User.Identity.Name);

            List<UserListViewModel> model = new List<UserListViewModel>();
            model = user.Select(u => new UserListViewModel
            {
                Id = u.Id,
                EId = u.EId,
                EName = u.EName,
                UserName = u.UserName

            }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserEditViewModel model = new UserEditViewModel();
            model.Roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.EId = user.EId;
                    model.EName = user.EName;
                    model.UserName = user.UserName;
                    model.EStreetAddress = user.EStreetAddress;
                    model.EPostalAddress = user.EPostalAddress;
                    model.EPostalCode = user.EPostalCode;
                    model.EJobTitle = user.EJobTitle;
                    model.RoleId = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.EName = model.EName;
                    user.UserName = model.UserName;
                    user.EStreetAddress = model.EStreetAddress;
                    user.EPostalAddress = model.EPostalAddress;
                    user.EPostalCode = model.EPostalCode;
                    user.EJobTitle = model.EJobTitle;
                    string existingRole = _userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = _roleManager.Roles.Single(r => r.Name == existingRole).Id;
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (existingRoleId != model.RoleId)
                        {
                            IdentityResult roleResult = await _userManager.RemoveFromRoleAsync(user, existingRole);
                            if (roleResult.Succeeded)
                            {
                                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.RoleId);
                                if (applicationRole != null)
                                {
                                    IdentityResult newRoleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                                    if (newRoleResult.Succeeded)
                                    {
                                        return RedirectToAction("Index");
                                    }
                                }
                            }
                        }
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

    }
}
