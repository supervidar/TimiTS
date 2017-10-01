using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models
{
    public class WorkParticipation
    {
        [Key]
        [Required]
        public int WPId { get; set; }

        [Display(Name = "Dato")]
        [DataType(DataType.DateTime)]
        public DateTime? DateTimeIn { get; set; }

        [Display(Name = "Dato")]
        [DataType(DataType.DateTime)]
        public DateTime? DateTimeOut { get; set; }


        [Display(Name = "Timer")]
        public double Hours { get; set; }

        [Display(Name = "Pause")]
        public double WPBreak { get; set; }

        [StringLength(255)]
        [Display(Name = "Kommentar")]
        public string Comment { get; set; }

        public bool Verified { get; set; }

        public bool ActiveSession { get; set; }

        [ForeignKey("CId")]
        [Display(Name = "Oppdragsgiver")]
        public int? ContracterId { get; set; }
        public Contractor  CId { get; set; }

        [ForeignKey("PId")]
        [Display(Name = "Prosjekt id")]
        public int? ProjectId { get; set; }
        public Project PId { get; set; }

        [Required]
        [ForeignKey("Id")]
        public string UserId { get; set; }
        public ApplicationUser Id { get; set; }

        [ForeignKey("WCId")]
        [Display(Name = "Arbeid utført")]
        public int? WorkCategoryId { get; set; }
        public WorkCategory WCId { get; set; }

        [ForeignKey("WTId")]
        public int? WorkTypeId { get; set; }
        public WorkType WTId { get; set; }
    }
}
