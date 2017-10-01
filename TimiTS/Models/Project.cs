using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Prosjekt id må fylles ut")]
        [Display(Name = "Prosjekt id")]
        public int PId { get; set; }

        [Display(Name = "Prosjekt Navn")]
        [StringLength(255)]
        public string PName { get; set; }

        [Display(Name = "Startdato")]
        public string PStartDate { get; set; }

        [Display(Name = "Sluttdato")]
        public string PEndDate { get; set; }

        [Required(ErrorMessage = "Estimert tid for mur må fylles ut")]
        [Display(Name = "Mur")]
        public double PEstimateMasonry { get; set; }

        [Required(ErrorMessage = "Estimert tid for flis må fylles ut")]
        [Display(Name = "Flis")]
        public double PEstimateTile { get; set; }

        [Required(ErrorMessage = "Estimert tid for råbygg må fylles ut")]
        [Display(Name = "Råbygg")]
        public double PEstimateStructural { get; set; }

        [Required(ErrorMessage = "Estimert tid for utvendig må fylles ut")]
        [Display(Name = "Utvendig")]
        public double PEstimateExternal { get; set; }

        [Required(ErrorMessage = "Estimert tid for plating må fylles ut")]
        [Display(Name = "Plating")]
        public double PEstimatePlating { get; set; }

        [Required(ErrorMessage = "Estimert tid for isolering og stender må fylles ut")]
        [Display(Name = "Iso og stendr.")]
        public double PEstimateStender { get; set; }

        [Required(ErrorMessage = "Estimert tid for sluttarbeid må fylles ut")]
        [Display(Name = "Sluttarb.")]
        public double PEstimateFinalWork { get; set; }

        [Required(ErrorMessage = "Estimert tid for car/gar må fylles ut")]
        [Display(Name = "Car/gar")]
        public double PEstimateGarage { get; set; }

        [Required(ErrorMessage = "Estimert tid for montering må fylles ut")]
        [Display(Name = "Montering")]
        public double PEstimateAssembly { get; set; }

        [Required(ErrorMessage = "Estimert tid for annet må fylles ut. Om det ikke er estimert forventet tid i annet, skriv 0")]
        [Display(Name = "Annet")]
        public double PEstimateOther { get; set; }

        public string Detail
        {
            get
            {
                return "Prosjekt " + this.PId + " . " + this.PName;
            }
        }
    }
}
