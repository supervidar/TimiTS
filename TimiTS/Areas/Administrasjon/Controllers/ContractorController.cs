using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models;
using TimiTS.Models.IRepository;

namespace TimiTS.Areas.Administrasjon.Controllers
{
  
    public class ContractorController : DomainCrontroller
    {
        private readonly IContractorRepository _repository;

        public ContractorController(IContractorRepository repository)
        {
            _repository = repository;
        }

        // List contractors
        [HttpGet]
        public IActionResult Index()
        {           
            return View(_repository.GetAllContractors);
        }

        // Get contractor based on CId
        [HttpGet]
        public ViewResult Edit(int id)
        {
            return View(_repository.GetAllContractors
                .FirstOrDefault(p => p.CId == id));
        }

        //Edit contractor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveContractor(contractor);
                //TempData["message"] = $"{project.Detail}  har blitt lagret";
                return RedirectToAction("Index");
            }
            else
            {
                //There is something wrong with the data values
                return View(contractor);
            }
        }


        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        // Adds contractor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateContractor(contractor);
                return RedirectToAction("Index");
            }
            else
            {
                return View(contractor);
            }

        }

        //Get contractor
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return View(_repository.GetAllContractors
               .FirstOrDefault(p => p.CId == id));
        }

        //Delete contractor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                Contractor deleteContractor = _repository.DeleteContractor(id);
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }

        }

    }
}
