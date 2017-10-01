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
   
    public class ProjectController : DomainCrontroller
    {
        private readonly IProjectRepository _repository;
        private IWorkParticipationRepository _workRepository;

        public ProjectController(IProjectRepository repository,
            IWorkParticipationRepository workRepository)
        {
            _repository = repository;
            _workRepository = workRepository;
        }

        //List projects 
        [HttpGet]
        public IActionResult Index()
        {           
            return View(_repository.GetAllProjects);
        }

        //get project
        [HttpGet]
        public ViewResult Edit(int id)
        {
            return View(_repository.GetAllProjects
                .FirstOrDefault(p=>p.PId == id));
        }

        //Edit project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProject(project);
                //TempData["message"] = $"{project.Detail}  har blitt lagret";
                return RedirectToAction("Index");
            }
            else
            {
                //There is something wrong with the data values
                return View(project);
            }
        }

        
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        //Adds project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateProject(project);
                return RedirectToAction("Index");
            }
            else
            {
                return View(project);
            }
            
        }

        //get project
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return View(_repository.GetAllProjects
               .FirstOrDefault(p => p.PId == id));
        }

        //delete project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                Project deleteProject = _repository.DeleteProject(id);
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }
         
        }

        //get users time and work done on the choosen project 
        [HttpGet]
        public IActionResult ProjectOverview(int id)
        {

            var project = _repository.FindProject(id);
            
            var projectWork = (from o in _workRepository.WorkParticipations
                           where o.ProjectId == id
                           group o by new { o.DateTimeIn.Value.Year, o.DateTimeIn.Value.Month, o.Id.EName, o.Id.EId }
                           into w
                           select new ProjectWorkViewModel
                           {

                               Date = w.Key.Month,
                               EId = w.Key.EId,
                               EName = w.Key.EName,

                               WA1 = w.Where(p => p.WorkCategoryId == 1).Sum(x => Math.Round(x.Hours * 2) / 2),
                               WA2 = w.Where(p => p.WorkCategoryId == 2).Sum(x => Math.Round(x.Hours * 2) / 2),
                               WA3 = w.Where(p => p.WorkCategoryId == 3).Sum(x => Math.Round(x.Hours * 2) / 2),
                               WA4 = w.Where(p => p.WorkCategoryId == 4).Sum(x => Math.Round(x.Hours * 2) / 2),
                               WA5 = w.Where(p => p.WorkCategoryId == 5).Sum(x => Math.Round(x.Hours * 2) / 2),
                               WA6 = w.Where(p => p.WorkCategoryId == 6).Sum(x => Math.Round(x.Hours * 2) / 2),
                               WA7 = w.Where(p => p.WorkCategoryId == 7).Sum(x => Math.Round(x.Hours * 2) / 2),
                               WA8 = w.Where(p => p.WorkCategoryId == 8).Sum(x => Math.Round(x.Hours * 2) / 2),
                               WA9 = w.Where(p => p.WorkCategoryId == 9).Sum(x => Math.Round(x.Hours * 2) / 2),
                               WA10 = w.Where(p => p.WorkCategoryId == 10).Sum(x => Math.Round(x.Hours * 2) / 2)

                           });


            ViewBag.Id1 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 1 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id2 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 2 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id3 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 3 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id4 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 4 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id5 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 5 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id6 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 6 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id7 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 7 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id8 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 8 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id9 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 9 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.Id10 = _workRepository.WorkParticipations.Where(m => m.WorkCategoryId == 10 && m.ProjectId == id).Sum(m =>Math.Round(m.Hours * 2) / 2);
            ViewBag.TotalSum = ViewBag.Id1 + ViewBag.Id2 + ViewBag.Id3 + ViewBag.Id4 + ViewBag.Id5 + ViewBag.Id6 +
                              ViewBag.Id7 + ViewBag.Id8 + ViewBag.Id9 + ViewBag.Id10;

            ProjectAndWorkViewModel model = new ProjectAndWorkViewModel
            {
                Project = project,
                ProjectWork = projectWork.ToList().OrderBy(d=>d.Date)
            };

            return View(model);
        }

       

    }
}
