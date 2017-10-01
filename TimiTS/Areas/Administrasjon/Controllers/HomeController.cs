using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Areas.Administrasjon.Controllers
{
    
    public class HomeController : DomainCrontroller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }
    }
}
