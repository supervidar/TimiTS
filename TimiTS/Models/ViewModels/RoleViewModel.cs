using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Rollenavn")]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
