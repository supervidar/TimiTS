using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Areas.Tømrer.Controllers
{
    // restrict the controlles to the area and role
    [Area("Tømrer")]
    [Authorize(Roles = "Tømrer")]
    public abstract class DomainController : Controller
    {
    }
}
