using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models;
using TimiTS.Models.IRepository;
/// <summary>
/// Create absence based on tree different absence reasons
/// </summary>
namespace TimiTS.Areas.Tømrer.Controllers
{
   
    public class AbsenceController : DomainController
    {
        private ApplicationDbContext _context;
        private IWorkParticipationRepository _repository;

        public AbsenceController(
            ApplicationDbContext context,
            IWorkParticipationRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendAbsence(WorkParticipation absence)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    absence.DateTimeOut = DateTime.Now;
                    _repository.CreateWorkParticipation(absence);
                    return RedirectToAction("index","Work");                      
                }
                return View(absence);
            }
            catch
            {

                return View();
            }
            
        }


        [HttpGet]
        public IActionResult Sick()
        {
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
            new SelectListItem { Text = "7,5 t", Value = "7,5" }
             
        }, "Value", "Text");
            return View();           
        }

        [HttpGet]
        public IActionResult SickChild()
        {
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
            new SelectListItem { Text = "7,5 t", Value = "7,5" }

        }, "Value", "Text");
            return View();
        }

        [HttpGet]
        public IActionResult SickLeave()
        {
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
            new SelectListItem { Text = "7,5 t", Value = "7,5" }

        }, "Value", "Text");
            return View();
        }

    }
}
