using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles = "Rolleadministrator")]
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManger;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManger = roleManager;
        }


        //List Roles
        [HttpGet]
        public IActionResult Index()
        {
            // TODO: Implement Dependency injection if possible
            List<RoleListViewModel> roleListViewModel = new List<RoleListViewModel>();
            roleListViewModel = _roleManger.Roles.Select(r => new RoleListViewModel
            {
                RoleName = r.Name,
                Id = r.Id,
                RoleDescription = r.RoleDescription,
                NumberOfUsers = r.Users.Count

            }).ToList();
            return View(roleListViewModel);
        }

        //Get role
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            RoleViewModel model = new RoleViewModel();
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManger.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.RoleName = applicationRole.Name;
                    model.RoleDescription = applicationRole.RoleDescription;
                }
            }
            return View(model);
        }

        //edit role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(id);
                ApplicationRole applicationRole = isExist ? await _roleManger.FindByIdAsync(id) : new ApplicationRole();


                applicationRole.Name = model.RoleName;
                applicationRole.RoleDescription = model.RoleDescription;

                IdentityResult roleResult = isExist ? await _roleManger.UpdateAsync(applicationRole)
                    : await _roleManger.CreateAsync(applicationRole);

                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        //Adds role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = new ApplicationRole
                {
                    Name = model.RoleName,
                    RoleDescription = model.RoleDescription
                };

                IdentityResult roleResult = await _roleManger.CreateAsync(applicationRole);
                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
            }
            return View(model);
        }


        //get role
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManger.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    name = applicationRole.Name;
                }
            }
            return View(name);
        }

        //delete role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id, IFormCollection from)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManger.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    IdentityResult roleResult = _roleManger.DeleteAsync(applicationRole).Result;
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Role");
                    }
                }
            }
            return View();
        }
    }
}
