using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models
{
    public class WorkType
    {
        [Key]
        [Display(Name = "Arbeidstype Id")]
        public int WTId { get; set; }
        [Display(Name = "Arbeidstype")]
        public string WTType { get; set; }

        public string WTDetail
        {
            get
            {
                return this.WTId + " . " + this.WTType;
            }
        }
    }
}
