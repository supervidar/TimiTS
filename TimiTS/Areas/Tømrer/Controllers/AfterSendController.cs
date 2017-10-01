using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models;
using TimiTS.Models.IRepository;
/// <summary>
/// Alternative way to report work
/// </summary>
namespace TimiTS.Areas.Tømrer.Controllers
{
    
    public class AfterSendController : DomainController
    {
        private ApplicationDbContext _context;
        private IWorkParticipationRepository _reposetory;

        public AfterSendController(
            ApplicationDbContext contaxt,
            IWorkParticipationRepository reposetory)
        {
            _context = contaxt;
            _reposetory = reposetory;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProjectWork()
        {
            // list project by id andname and puts it in a ViewBag
            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "Detail");
            // list workcategories by ide and name and puts it in a ViewBag
            ViewBag.WorkCategoryId = new SelectList(_context.WorkCategories, "WCId", "WCDetail");
            // create ViewBag with data to be shown in the view
            ViewBag.Hours = new SelectList(
             new List<SelectListItem> {
             new SelectListItem { Text = "1,0 t", Value = "1,0" },
             new SelectListItem { Text = "1,5 t", Value = "1,5" },
             new SelectListItem { Text = "2,0 t", Value = "2,0" },
             new SelectListItem { Text = "2,5 t", Value = "2,5" },
             new SelectListItem { Text = "3,0 t", Value = "3,0" },
             new SelectListItem { Text = "3,5 t", Value = "3,5" },
             new SelectListItem { Text = "4,0 t", Value = "4,0" },
             new SelectListItem { Text = "4,5 t", Value = "4,5" },
             new SelectListItem { Text = "5,0 t", Value = "5,0" },
             new SelectListItem { Text = "5,5 t", Value = "5,5" },
             new SelectListItem { Text = "6,0 t", Value = "6,0" },
             new SelectListItem { Text = "6,5 t", Value = "6,5" },
             new SelectListItem { Text = "7,0 t", Value = "7,0" },
             new SelectListItem { Text = "7,5 t", Value = "7,5" },
             new SelectListItem { Text = "8,0 t", Value = "8,0" },
             new SelectListItem { Text = "8,5 t", Value = "8,5" },
             new SelectListItem { Text = "9,0 t", Value = "9,0" },
             new SelectListItem { Text = "9,5 t", Value = "9,5" },
             new SelectListItem { Text = "10,0 t", Value = "10,0" },
             new SelectListItem { Text = "10,5 t", Value = "10,5" },
             new SelectListItem { Text = "11,0 t", Value = "11,0" },
             new SelectListItem { Text = "11,5 t", Value = "11,5" },
             new SelectListItem { Text = "12,0 t", Value = "12,0" }
        }, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProjectWork(WorkParticipation workParticipation)
        {
            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "Detail", workParticipation.ProjectId);
            ViewBag.WorkCategoryId = new SelectList(_context.WorkCategories, "WCId", "WCDetail", workParticipation.WorkCategoryId);
            try
            {
                // valtidates if requirements is met
                if (ModelState.IsValid)
                {
                    // Insert new data to database, returns to index
                    workParticipation.DateTimeOut = DateTime.Now;                    
                    _reposetory.CreateWorkParticipation(workParticipation);
                    return RedirectToAction("Index", "Work");
                }
                // Returns the value of the fields back to the view
                return View(workParticipation);
            }
            catch
            {
                // Returns the value of the fields back to the view
                return View();
            }
        }

        [HttpGet]
        public IActionResult HourWork()
        { 
            // list project by id and name and puts it in a ViewBag
            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "Detail");
            // list contractor by name and puts it in a ViewBag
            ViewBag.ContracterId = new SelectList(_context.Contractors, "CId", "CName");
            // create ViewBag with data to be shown in the view
            ViewBag.Hours = new SelectList(
             new List<SelectListItem> {
             new SelectListItem { Text = "1,0 t", Value = "1,0" },
             new SelectListItem { Text = "1,5 t", Value = "1,5" },
             new SelectListItem { Text = "2,0 t", Value = "2,0" },
             new SelectListItem { Text = "2,5 t", Value = "2,5" },
             new SelectListItem { Text = "3,0 t", Value = "3,0" },
             new SelectListItem { Text = "3,5 t", Value = "3,5" },
             new SelectListItem { Text = "4,0 t", Value = "4,0" },
             new SelectListItem { Text = "4,5 t", Value = "4,5" },
             new SelectListItem { Text = "5,0 t", Value = "5,0" },
             new SelectListItem { Text = "5,5 t", Value = "5,5" },
             new SelectListItem { Text = "6,0 t", Value = "6,0" },
             new SelectListItem { Text = "6,5 t", Value = "6,5" },
             new SelectListItem { Text = "7,0 t", Value = "7,0" },
             new SelectListItem { Text = "7,5 t", Value = "7,5" },
             new SelectListItem { Text = "8,0 t", Value = "8,0" },
             new SelectListItem { Text = "8,5 t", Value = "8,5" },
             new SelectListItem { Text = "9,0 t", Value = "9,0" },
             new SelectListItem { Text = "9,5 t", Value = "9,5" },
             new SelectListItem { Text = "10,0 t", Value = "10,0" },
             new SelectListItem { Text = "10,5 t", Value = "10,5" },
             new SelectListItem { Text = "11,0 t", Value = "11,0" },
             new SelectListItem { Text = "11,5 t", Value = "11,5" },
             new SelectListItem { Text = "12,0 t", Value = "12,0" }
        }, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HourWork(WorkParticipation workParticipation)
        {
            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "PName", workParticipation.ProjectId);
            ViewBag.ContracterId = new SelectList(_context.Contractors, "CId", "CName", workParticipation.ContracterId);
            try
            {
                // valtidates if requirements is met
                if (ModelState.IsValid)
                {
                    // Insert new data to database, returns to index
                    workParticipation.DateTimeOut = DateTime.Now;
                    _reposetory.CreateWorkParticipation(workParticipation);
                    return RedirectToAction("Index", "Work");
                }
                // Returns the value of the fields back to the view
                return View(workParticipation);
            }
            catch
            {
                // Returns the value of the fields back to the view
                return View(workParticipation);
            }
        }
    }
}
