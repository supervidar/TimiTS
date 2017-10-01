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

//TODO IMPLEMENTER dependency injection
namespace TimiTS.Areas.Tømrer.Controllers
{
     
    public class WorkController : DomainController
    {
        private readonly IWorkParticipationRepository _repository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private int wId;

        public WorkController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> usermanager,
            IWorkParticipationRepository repository)
        {
            _context = context;
            _userManager = usermanager;
            _repository = repository;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
          
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
                     

            if (_context.WorkParticipations.Where(u => u.UserId == userId && u.ActiveSession == true) != null)
            {
                wId = _context.WorkParticipations.Where(u => u.UserId == userId && u.ActiveSession == true).Max(w => w.WPId);
                var workParticipation = _repository.FindWorkParticipation(wId);
                if (wId != 0 && workParticipation.ProjectId == null && workParticipation.DateTimeOut == null)
                {
                    return RedirectToAction("CheckIn");
                }
            }
            else
            {
                return View();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(WorkParticipation workParticipation)
        {   

            //Insert new data to database
            if (Request.Form["BtnStart"] != String.IsNullOrEmpty(Request.Form["BtnStart"]))
            {
                workParticipation.ActiveSession = true;
                workParticipation.DateTimeIn = DateTime.Now;
                _repository.CreateWorkParticipation(workParticipation);
                return RedirectToAction("CheckIn");
            }
            else
                return View();
           
        }

        [HttpGet]
        public async Task<IActionResult> CheckIn()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var userId = user?.Id;

            wId = _context.WorkParticipations.Where(u => u.UserId == userId && u.ActiveSession == true).Max(wp => wp.WPId);
            if (wId == 0)
            {
                return NotFound();
            }
            var workParticipation = _repository.FindWorkParticipation(wId);

            if (workParticipation == null)
            {
                return NotFound();
            }

            return View(workParticipation);

        }

        [HttpGet]
        public IActionResult ProjectWork(int id)
        {
           
            // list project by id andname and puts it in a ViewBag
            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "Detail");
            // list workcategories by ide and name and puts it in a ViewBag
            ViewBag.WorkCategoryId = new SelectList(_context.WorkCategories, "WCId", "WCDetail");
            // makes list of values an put them in a ViewBag
            ViewBag.WPBreak = new SelectList(
                new List<SelectListItem> {
                    new SelectListItem { Text = "Ingen pause", Value = "0" },
                 new SelectListItem { Text = "15 minutt -> 0,25", Value = "0,25" },
                 new SelectListItem { Text = "30 minutt -> 0,5", Value = "0,5" },
                 new SelectListItem { Text = "45 minutt -> 0,75", Value = "0,75" },
                 new SelectListItem { Text = "60 minutt -> 1,0", Value = "1,0" }
               }, "Value", "Text");
           
            WorkParticipation workParticipation = _repository.FindWorkParticipation(id);

            if (workParticipation == null)
            {
                // returns error if not found
                return NotFound();
            }

            // returns data to view
            return View(workParticipation);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProjectWork(WorkParticipation workParticipation)
        {
            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "Detail", workParticipation.ProjectId);
            ViewBag.WorkCategoryId = new SelectList(_context.WorkCategories, "WCId", "WCDetail", workParticipation.WorkCategoryId);

            try
            {
                if (ModelState.IsValid)
                {                   
                    workParticipation.ActiveSession = false;
                    _repository.SaveWorkParticipation(workParticipation);
                    return RedirectToAction("ProjectVerification");
                }
                else
                {
                    return View(workParticipation);
                }
            }
            catch 
            {
                return View(workParticipation);
            }
            
        }

        [HttpGet]
        public IActionResult HourWork(int id)
        {           
            // gets list of project and represent it with PId and PName
            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "Detail");
            // Gets list of contractor and represnt it with CId and CName
            ViewBag.ContracterId = new SelectList(_context.Contractors, "CId", "CName");
            // makes list of values and put them in a ViewBag
            ViewBag.WPBreak = new SelectList(
               new List<SelectListItem> {
                      new SelectListItem { Text = "Ingen pause", Value = "0" },
                 new SelectListItem { Text = "15 minutt", Value = "0,25" },
                 new SelectListItem { Text = "30 minutt", Value = "0,5" },
                 new SelectListItem { Text = "45 minutt", Value = "0,75" },
                 new SelectListItem { Text = "60 minutt", Value = "1,0" }
              }, "Value", "Text");           
           
            WorkParticipation workParticipation = _repository.FindWorkParticipation(id);

            if (workParticipation == null)
            {
                // returns error when not found
                return NotFound();
            }

            // returns data to view
            return View(workParticipation);

        }

        // POST: Employe/HourWork/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HourWork(WorkParticipation workParticipation)
        { 

            ViewBag.ProjectId = new SelectList(_context.Projects, "PId", "Detail", workParticipation.ProjectId);            
            ViewBag.ContracterId = new SelectList(_context.Contractors, "CId", "CName", workParticipation.ContracterId);

            try
            {
                if (ModelState.IsValid)
                {
                    workParticipation.ActiveSession = false;
                    _repository.SaveWorkParticipation(workParticipation);
                    return RedirectToAction("HourWorkVerification");
                }
                else
                {
                    return View(workParticipation);
                }
            }
            catch
            {
                return View(workParticipation);
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> ProjectVerification()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var userId = user?.Id;

            wId = _context.WorkParticipations.Where(u => u.UserId == userId).Max(w => w.WPId);
            if (wId == 0)
            {
                return NotFound();
            }
            WorkParticipation workParticipation = _repository.FindWorkParticipation(wId);

            if (workParticipation == null)
            {
                return NotFound();
            }

            return View(workParticipation);
        }

        [HttpGet]
        public async Task<IActionResult> HourWorkVerification()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var userId = user?.Id;

            wId = _context.WorkParticipations.Where(u => u.UserId == userId).Max(w => w.WPId);
            if (wId == 0)
            {
                return NotFound();
            }

            WorkParticipation workParticipation = _repository.FindWorkParticipation(wId);

            if (workParticipation == null)
            {
                return NotFound();
            }


            return View(workParticipation);
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
