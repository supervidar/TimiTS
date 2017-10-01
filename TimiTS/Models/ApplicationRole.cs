using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string RoleDescription { get; set; }
    }
}
