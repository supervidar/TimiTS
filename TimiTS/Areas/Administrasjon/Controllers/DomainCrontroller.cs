using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Areas.Administrasjon.Controllers
{
    //restrict the controlles to area an role
    [Area("Administrasjon")]
    [Authorize(Roles = "Administrasjon")]
    public abstract class DomainCrontroller : Controller
    {
    }
}
