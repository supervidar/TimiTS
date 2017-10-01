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
  
    public class WorkController : DomainCrontroller
    {
        private readonly IWorkParticipationRepository _repository;
        private ApplicationDbContext _context;

        public WorkController(
            IWorkParticipationRepository repository,
            ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        //Get work done by employee. Order by year and date.
        [HttpGet]
        public IActionResult Index()
        {
            
            var workList = (from o in _repository.WorkParticipations                            
                            group o by new { o.DateTimeIn.Value.Year ,o.DateTimeIn.Value.Month, o.Id.EName , o.Verified}
                        into work
                            select new WorkListViewModel
                            {
                                YearInt = work.Key.Year,
                                DateInt = work.Key.Month,                                                                
                                UserId = work.Key.EName,
                                Verified = work.Key.Verified,
                                TotalHours = work.Sum(s => Math.Round(s.Hours * 2) / 2)

                            });

            return View(workList.OrderByDescending(d=>d.YearInt).OrderByDescending(d=>d.DateInt));
        }

        // get Work
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

        //edit work
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

        //get work
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

        //delete work
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

        //get all work and listed by year
        [HttpGet]
        public IActionResult WorkArchive()
        {
            var list = (from o in _context.WorkParticipations
                        group o by new { o.DateTimeIn.Value.Year }
                      into wrk
                        select new SumViewModel
                        {
                            Date = wrk.Key.Year,

                        }).ToList();
            
            return View(list);
        }

        //Get work from all employees and arrange it in a table // TODO: Find a better solution then ViewBag.
        [HttpGet]
        public IActionResult WorkOverView(int id)
        {
            ViewBag.Year = id;

            var work = (from o in _repository.GetAllWorkParticipation
                        where o.DateTimeIn.Value.Year == id
                        group o by new { o.Id.EId, o.Id.EName, o.DateTimeIn.Value.Year }
                         into w
                        select new SumViewModel
                        {

                            EId = w.Key.EId,
                            EName = w.Key.EName,

                            Jan = w.Where(p => p.DateTimeIn.Value.Month == 1).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Feb = w.Where(p => p.DateTimeIn.Value.Month == 2).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Mar = w.Where(p => p.DateTimeIn.Value.Month == 3).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Apr = w.Where(p => p.DateTimeIn.Value.Month == 4).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Mai = w.Where(p => p.DateTimeIn.Value.Month == 5).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Jul = w.Where(p => p.DateTimeIn.Value.Month == 6).Sum(p =>Math.Round(p.Hours * 2) / 2),

                            HalfSum = w.Where(p => p.DateTimeIn.Value.Month <= 6).Sum(p =>Math.Round(p.Hours * 2) / 2),

                            Jun = w.Where(p => p.DateTimeIn.Value.Month == 7).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Aug = w.Where(p => p.DateTimeIn.Value.Month == 8).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Sep = w.Where(p => p.DateTimeIn.Value.Month == 9).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Okt = w.Where(p => p.DateTimeIn.Value.Month == 10).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Nov = w.Where(p => p.DateTimeIn.Value.Month == 11).Sum(p =>Math.Round(p.Hours * 2) / 2),
                            Des = w.Where(p => p.DateTimeIn.Value.Month == 1).Sum(p =>Math.Round(p.Hours * 2) / 2),

                            TotSum = w.Where(p => p.DateTimeIn.Value.Month <= 12).Sum(p =>Math.Round(p.Hours * 2) / 2)

                        }).ToList();

            ViewBag.Id1 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 1).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id2 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 2).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id3 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 3).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id4 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 4).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id5 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id6 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 6).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.HalfSum = ViewBag.Id1 + ViewBag.Id2 + ViewBag.Id3 + ViewBag.Id4 + ViewBag.Id5 + ViewBag.Id6;
            ViewBag.Id7 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 7).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id8 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 8).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id9 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 9).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id10 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 10).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id11 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 11).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id12 = _repository.GetAllWorkParticipation.Where(p => p.DateTimeIn.Value.Month == 12).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.TotalSum = ViewBag.Id1 + ViewBag.Id2 + ViewBag.Id3 + ViewBag.Id4 + ViewBag.Id5 + ViewBag.Id6 +
                              ViewBag.Id7 + ViewBag.Id8 + ViewBag.Id9 + ViewBag.Id10 + ViewBag.Id11 + ViewBag.Id12;

            return View(work);


        }
    }
}
