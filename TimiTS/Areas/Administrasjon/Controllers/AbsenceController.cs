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
   
    public class AbsenceController : DomainCrontroller
    {
        private readonly IWorkParticipationRepository _repository;
        private ApplicationDbContext _context;
     

        public AbsenceController(
            IWorkParticipationRepository repository,
            ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // List absence // TODO: make similar to Administration/Work/Index
        [HttpGet]
        public IActionResult Index(int page = 1)
        {

            return View(_repository.Absneces);
        }

        //Get absence based on WPId
        [HttpGet]
        public IActionResult Edit(int id)
        {
            WorkParticipation absence = _repository.FindWorkParticipation(id);
            if (absence == null)
            {
                return NotFound();
            }
            return View(absence);
        }

        //Edit absence based on WPId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(WorkParticipation absence)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.SaveWorkParticipation(absence);
                    return RedirectToAction("Index");
                }
                return View(absence);
            }
            catch 
            {

                return View(absence);
            }            
        }

        //Get absence based on WPId
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var absence = _repository.FindWorkParticipation(id);
            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }

        //Delete absence based on WPId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                WorkParticipation deleteAbsnece = _repository.DeleteWorkParticipation(id);
                return RedirectToAction("Index");
            }
            catch 
            {

                return View();
            }
        }

        //Filter work based on year
        [HttpGet]
        public IActionResult AbsenceArchive()
        {
            
            var absenceDate = (from a in _context.WorkParticipations
                               where a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5
                               group a by new { a.DateTimeIn.Value.Year }
                               into abs
                               select new SumViewModel
                               {
                                   Date = abs.Key.Year
                               }).ToList();

            return View(absenceDate);
        }

        //Get absence from all employees and arrange it in a table // TODO: Find a better solution then ViewBag.
        [HttpGet]
        public IActionResult AbsenceOverview(int id)
        {
            ViewBag.Year = id;

            var absenceOverview = (from ab in _context.WorkParticipations
                                   where ((ab.DateTimeIn.Value.Year == id) && (ab.WorkTypeId == 3 || ab.WorkTypeId == 4 || ab.WorkTypeId == 5))
                                   group ab by new { ab.Id.EId, ab.Id.EName, ab.DateTimeIn.Value.Year }
                                   into absence
                                   select new SumViewModel
                                   {
                                       EId = absence.Key.EId,
                                       EName = absence.Key.EName,
                                       Date = absence.Key.Year,

                                       Sick = absence.Where(m => m.WorkTypeId == 3).Sum(p => p.Hours),
                                       SickChild = absence.Where(m => m.WorkTypeId == 4).Sum(p => p.Hours),
                                       SickLeave = absence.Where(m => m.WorkTypeId == 5).Sum(p => p.Hours),

                                       Jan = absence.Where(p => p.DateTimeIn.Value.Month == 1).Sum(p =>p.Hours),
                                       Feb = absence.Where(p => p.DateTimeIn.Value.Month == 2).Sum(p =>p.Hours),
                                       Mar = absence.Where(p => p.DateTimeIn.Value.Month == 3).Sum(p =>p.Hours),
                                       Apr = absence.Where(p => p.DateTimeIn.Value.Month == 4).Sum(p =>p.Hours),
                                       Mai = absence.Where(p => p.DateTimeIn.Value.Month == 5).Sum(p =>p.Hours),
                                       Jun = absence.Where(p => p.DateTimeIn.Value.Month == 6).Sum(p =>p.Hours),

                                       HalfSum = absence.Where(p => p.DateTimeIn.Value.Month <= 6).Sum(p =>p.Hours),

                                       Jul = absence.Where(p => p.DateTimeIn.Value.Month == 7).Sum(p =>p.Hours),
                                       Aug = absence.Where(p => p.DateTimeIn.Value.Month == 8).Sum(p =>p.Hours),
                                       Sep = absence.Where(p => p.DateTimeIn.Value.Month == 9).Sum(p =>p.Hours),
                                       Okt = absence.Where(p => p.DateTimeIn.Value.Month == 10).Sum(p =>p.Hours),
                                       Nov = absence.Where(p => p.DateTimeIn.Value.Month == 11).Sum(p =>p.Hours),
                                       Des = absence.Where(p => p.DateTimeIn.Value.Month == 1).Sum(p => p.Hours),

                                       TotSum = absence.Where(p => p.DateTimeIn.Value.Month <= 12).Sum(p => p.Hours)

                                   }).ToList();


            ViewBag.Id1 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 1).Sum(m => Math.Round(m.Hours * 2) / 2);
            ViewBag.Id2 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 2).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id3 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 3).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id4 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 4).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id5 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 5).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id6 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 6).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.HalfSum = ViewBag.Id1 + ViewBag.Id2 + ViewBag.Id3 + ViewBag.Id4 + ViewBag.Id5 + ViewBag.Id6;
            ViewBag.Id7 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 7).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id8 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 8).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id9 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 9).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id10 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 10).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id11 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 11).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id12 = _repository.Absneces.Where(p => p.DateTimeIn.Value.Month == 12).Where(a => a.WorkTypeId == 3 || a.WorkTypeId == 4 || a.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.TotalSum = ViewBag.Id1 + ViewBag.Id2 + ViewBag.Id3 + ViewBag.Id4 + ViewBag.Id5 + ViewBag.Id6 +
                              ViewBag.Id7 + ViewBag.Id8 + ViewBag.Id9 + ViewBag.Id10 + ViewBag.Id11 + ViewBag.Id12;

            ViewBag.Child = _context.WorkParticipations.Where(p => p.WorkTypeId == 4).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Sick = _context.WorkParticipations.Where(p => p.WorkTypeId == 3).Sum(m => Math.Round(m.Hours * 2) / 2);
            ViewBag.SickLeave = _context.WorkParticipations.Where(p => p.WorkTypeId == 5).Sum(m =>Math.Round(m.Hours * 2) / 2);

            return View(absenceOverview);
        }
    }
}
