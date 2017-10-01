using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models
{
    public class Feedback
    {
        [Key]
        [Required]
        public int FId { get; set; }

        public string FUser { get; set; }

        [ForeignKey("BCId")]
        public int? FeedbackCategoryId { get; set; }
        public FeedbackCategory FCId { get; set; }

        public string FComment { get; set; }

        [HiddenInput]
        public int FRating { get; set; }
    }
}
