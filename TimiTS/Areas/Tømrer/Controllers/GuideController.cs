using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// User guide for Timi
/// </summary>
namespace TimiTS.Areas.Tømrer.Controllers
{     
    public class GuideController : DomainController
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
