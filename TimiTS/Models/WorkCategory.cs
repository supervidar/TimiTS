using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models
{
    public class WorkCategory
    {
        [Key]
        [Display(Name = "Arbeid ID")]
        public int WCId { get; set; }

        [Display(Name = "Utført arbeid:")]
        public string WCPerformed { get; set; }

        public string WCDetail
        {
            get
            {
                return this.WCId + " . " + this.WCPerformed;
            }
        }
    }
}
