using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Ansatt Id må fylles inn")]
        [Display(Name = "Ansatt Id")]
        public int EId { get; set; }

        [Required(ErrorMessage = "Navn på ansatt må fylles inn")]
        [StringLength(255)]
        [Display(Name = "Navn")]
        public string EName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Post")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }      

        public string Id { get; set; }
        
        public List<SelectListItem> Roles { get; set; }
        public string RoleId { get; set; }
    }
}
