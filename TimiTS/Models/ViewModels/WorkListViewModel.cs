using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models.ViewModels
{
    public class WorkListViewModel
    {
        public int WPId { get; set; }

        public string UserId { get; set; }

        public bool Verified { get; set; }

        public int DateInt { get; set; }
        public int YearInt { get; set; }
        public DateTime? Date { get; set; }
        public string Comment { get; set; }
        public int? ProjectId { get; set; }
        public string Contractor { get; set; }
        public string WorkType { get; set; }
        public double Hour { get; set; }
        public double TotalHours { get; set; }
    }
}
