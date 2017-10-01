using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models;
using TimiTS.Models.IRepository;
using TimiTS.Models.ViewModels;

namespace TimiTS.Areas.Administrasjon.Controllers
{

    [Area("Administrasjon")]
    public class UserController : Controller
    {
        private  UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private  RoleManager<ApplicationRole> _roleManager;
        private IWorkParticipationRepository _repository;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext context,
            IWorkParticipationRepository repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _repository = repository;
        }

        //List users
        [HttpGet]
        public IActionResult Index()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            if (model != null)
            {
                model = _userManager.Users.Select(u => new UserListViewModel
                {
                    Id = u.Id,
                    EId = u.EId,
                    EName = u.EName,
                    UserName = u.UserName

                }).ToList();
                return View(model);
            }

            return View();


        }

        [HttpGet]
        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel
            {
                Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }).ToList()
            };
            return View(model);
            
        }

        //Create User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {                  
                    EId = registerViewModel.EId,
                    EName = registerViewModel.EName,
                    UserName = registerViewModel.UserName                   
                    
                };

                // cheack if the user is added to the database
                IdentityResult result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
                if (result.Succeeded)
                {
                    ApplicationRole applicationRole = await _roleManager.FindByIdAsync(registerViewModel.RoleId);
                    if (applicationRole != null)
                    {
                        // check if the role is added to the database
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index");

                        }                       
                    }                   
                }               
            }
            return View(registerViewModel);
        }

        //get user
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserEditViewModel model = new UserEditViewModel
            {
                Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }).ToList()
            };

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


        //Edit user
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

        //get user
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    name = applicationUser.EName;
                }
            }
            return View(name);
        }

        //delete user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");

                    }
                }
            }
            return View();
        }
        //TODO: ALTERNATIVE WAY TO SHOW WORK DONE BY EMPLOYEE - SEE WORKOVERVIEW
        //[HttpGet]
        //public IActionResult UserWorkDetails(string id)
        //{
        //    var workUser =
        //        (from u in _repository.WorkParticipations
        //         where (u.UserId == id)
        //         group u by new { u.DateTimeIn, u.WTId.WTType, u.ProjectId, u.CId.CName, }
        //         into work
        //         select new WorkListViewModel
        //         {
        //             Date = work.Key.DateTimeIn,
        //             WorkType = work.Key.WTType,
        //             ProjectId = work.Key.ProjectId,
        //             Contractor = work.Key.CName,
                     
        //         });
                 
        //    return View();
        //}

        #region Helpers
        private Task<ApplicationUser> GetCurrentUserAsync()
        {

            return _userManager.GetUserAsync(HttpContext.User);

        }
        private string GetCurrentUserId()
        {
            var currentUser = GetCurrentUserAsync();
            var userId = currentUser?.Id;

            return userId.ToString();

        }

        #endregion
    }
}
