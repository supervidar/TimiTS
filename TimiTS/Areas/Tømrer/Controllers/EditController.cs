using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models;
using TimiTS.Models.IRepository;
/// <summary>
/// Edit work
///* This code us currently not in use.
///* See WorkOverview.
/// </summary>
namespace TimiTS.Areas.Tømrer.Controllers
{
    
    public class EditController : DomainController
    {
        private ApplicationDbContext _context;
        private IWorkParticipationRepository _repository;
        private UserManager<ApplicationUser> _userManager;
        

        public EditController(
            ApplicationDbContext context,
            IWorkParticipationRepository repository,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;

            return View(_repository.WorkParticipations.Where(u => u.UserId == userId));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // list contractor by name and puts it in a ViewBag
            ViewBag.ContracterId = new SelectList(_context.Contractors, "CId", "CName");
            // list project by id and name and puts it in a ViewBag
            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "Detail");
            // list workcategories by id and name and puts it in a ViewBag
            ViewBag.WorkCategoryId = new SelectList(_context.WorkCategories, "WCId", "WCDetail");

            var work = _repository.FindWorkParticipation(id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(WorkParticipation workParticipation)
        {
            ViewBag.ContracterId = new SelectList(_context.Contractors, "CId", "CName", workParticipation.ContracterId);
            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "Detail", workParticipation.ProjectId);
            ViewBag.WorkCategoryId = new SelectList(_context.WorkCategories, "WCId", "WCDetail", workParticipation.WorkCategoryId);

            try
            {
                // Checks if the model requirements is valid.
                if (ModelState.IsValid)
                {
                    _repository.SaveWorkParticipation(workParticipation);
                    return RedirectToAction("Index");
                }
                return View(workParticipation);
            }
            catch
            {
                return View(workParticipation);
            }            
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //finds work to delelete based on value in parameter
            var workParticipation = _repository.FindWorkParticipation(id);
            if (workParticipation == null)
            {
                return NotFound();
            }
            return View(workParticipation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteWorkParticipation(id);
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }
           
        }


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
