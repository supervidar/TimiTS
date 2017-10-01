using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add profile data for application users by adding properties to the ApplicationUser class
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
    }
}
