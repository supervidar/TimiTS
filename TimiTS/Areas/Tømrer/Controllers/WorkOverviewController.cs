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

namespace TimiTS.Areas.Tømrer.Controllers
{
     // TODO: Update/Edit user not wokring correctly
    public class WorkOverviewController : DomainController
    {
        private ApplicationDbContext _context;
        private IWorkParticipationRepository _repository;
        private UserManager<ApplicationUser> _userManager;

        public WorkOverviewController(
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
            //TODO: DateTime.Now.Year can be a problem in desember if the user hasn't verified the worklogs. Will only show list from current year
            var workList = (from o in _context.WorkParticipations
                        where ((o.UserId == userId) && (o.WorkTypeId == 1 || o.WorkTypeId == 2) && (o.DateTimeIn.Value.Year == DateTime.Now.Year))
                        group o by new { o.DateTimeIn.Value.Month}
                        into work
                        select new WorkListViewModel
                        {
                            DateInt = work.Key.Month,
                            TotalHours = work.Sum(s=> Math.Round(s.Hours * 2) / 2)

                        });

            return View(workList.OrderByDescending(d=>d.DateInt));
        }

        [HttpGet]
        public async Task<IActionResult> MonthlyWorkLog(int id)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;

            var work = _repository.WorkParticipations.Where(u => u.UserId == userId).Where(d=>d.DateTimeIn.Value.Month == id);
            if (work == null)
            {
                return NotFound();
            }
            ViewBag.Total = work.Sum(w => w.Hours);
            return View(work.OrderBy(d=>d.DateTimeIn));
        }
        //TODO: MAKE IT POSSIBLE TO CHANGE THE Verified ATTRIBUTE FOR THE WHOLE WORK MONTH
        [HttpPost]
        public async Task<IActionResult> MonthlyWorkLog(int id, WorkParticipation workParticipation)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;


            //Insert new data to database
            if (Request.Form["BtnVerify"] != String.IsNullOrEmpty(Request.Form["BtnVerify"]))
            {
                var work = _repository.WorkParticipations.Where(u => u.UserId == userId).Where(d => d.DateTimeIn.Value.Month == id);

                //foreach (var item in work)
                //{
                //    if (item.Verified == false)
                //    {
                //        item.Verified = true;
                //        _repository.SaveWorkParticipation(item);
                //    }
                //}
                //for (int i = work.Count() - 1; i >= 0; i--)
                //{
                //    workParticipation.WPId = work.WPId;
                //    workParticipation.DateTimeIn = workParticipation.DateTimeIn;
                //    workParticipation.DateTimeOut = workParticipation.DateTimeOut;
                //    workParticipation.Hours = workParticipation.Hours;
                //    workParticipation.WPBreak = workParticipation.WPBreak;
                //    workParticipation.Comment = workParticipation.Comment;
                //    workParticipation.ActiveSession = workParticipation.ActiveSession;
                //    workParticipation.ContracterId = workParticipation.ContracterId;
                //    workParticipation.ProjectId = workParticipation.ProjectId;
                //    workParticipation.UserId = workParticipation.UserId;
                //    workParticipation.WorkCategoryId = workParticipation.WorkCategoryId;
                //    workParticipation.WorkTypeId = workParticipation.WorkTypeId;
                //    workParticipation.Verified = true;
                //    //work.Verified = true;
                //    //workParticipation.Verified = true;
                //    _repository.WorkParticipationValidationUpdate(workParticipation);
                //}
                //_repository.SaveWorkParticipation(work);
                return RedirectToAction("Index");
            }
            else

                return View();
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

        //TODO: Find a way to delete with fewer steps. 
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
