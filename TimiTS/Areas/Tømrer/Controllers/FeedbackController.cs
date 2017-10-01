using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models;
using TimiTS.Models.IRepository;
/// <summary>
/// Feedbackhub
/// </summary>
namespace TimiTS.Areas.Tømrer.Controllers
{
     
    public class FeedbackController : DomainController
    {
        private ApplicationDbContext _context;
        private IFeedbackRepository _repository;
        public FeedbackController(
            ApplicationDbContext context,
            IFeedbackRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repository.GetAllFeedback);
        }

        
        [HttpGet]
        public IActionResult CreateFeedback()
        {
            ViewBag.FeedbackCategoryId = new SelectList(_context.FeedbackCategories, "FCId", "FCDetail");
            
            return View();
        }

        //Create feeback
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFeedback(Feedback feedback)
        {
            ViewBag.FeedbackCategoryId = new SelectList(_context.FeedbackCategories, "FCId", "FCDetail", feedback.FeedbackCategoryId);

            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CreateFeedback(feedback);
                    return RedirectToAction("Index", "Work");
                }
                return View(feedback);
            }
            catch 
            {

                return View();
            }
            
        }
    }
}
