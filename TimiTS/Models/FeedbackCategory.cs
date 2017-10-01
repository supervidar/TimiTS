using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models
{
    public class FeedbackCategory
    {
        [Key]
        public int FCId { get; set; }

        public string FCCategory { get; set; }

        public string FCDetail
        {
            get
            {
                return this.FCId + " . " + this.FCCategory;
            }
        }
    }
}
