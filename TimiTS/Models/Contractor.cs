using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models
{
    public class Contractor
    {
        [Key]
        [Required]
        public int CId { get; set; }

        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Oppdragsgiver's navn må fylles ut")]
        [StringLength(255)]
        public string CName { get; set; }

        [Display(Name = "Gatenavn")]
        [StringLength(255)]
        public string CStreetAddress { get; set; }

        [Required(ErrorMessage = "Oppdragsgiver's postnr må fylles ut")]
        [Display(Name = "Postnr.")]
        [StringLength(255)]
        public string CPostalCode { get; set; }

        [Required(ErrorMessage = "Oppdragsgiver's Poststed må fylles ut")]
        [Display(Name = "Poststed")]
        [StringLength(255)]
        public string CPostalAddress { get; set; }

        [Display(Name = "E-post")]
        [StringLength(255)]
        public string CEmail { get; set; }

        [Display(Name = "Organisasjonsnr.")]
        [StringLength(255)]
        public string COrgNr { get; set; }
    }
}
