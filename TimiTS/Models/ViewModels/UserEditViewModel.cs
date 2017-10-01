using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models.ViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Ansatt Id må fylles inn")]
        [Display(Name = "Ansatt Id")]
        public int EId { get; set; }

        [Required(ErrorMessage = "Navn på ansatt må fylles inn")]
        [StringLength(255)]
        [Display(Name = "Navn")]
        public string EName { get; set; }

        [StringLength(255)]
        [Display(Name = "Gatenavn")]
        public string EStreetAddress { get; set; }

        [StringLength(255)]
        [Display(Name = "Postnummer")]
        public string EPostalCode { get; set; }

        [StringLength(255)]
        [Display(Name = "Poststed")]
        public string EPostalAddress { get; set; }

        [StringLength(255)]
        [Display(Name = "Stilling")]
        public string EJobTitle { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Post")]
        public string UserName { get; set; }


        public List<SelectListItem> Roles { get; set; }
        public string RoleId { get; set; }
       
    }
}
