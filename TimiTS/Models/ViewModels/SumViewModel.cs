using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models.ViewModels
{
    public class SumViewModel
    {
        public double Jan { get; set; }
        public double Feb { get; set; }
        public double Mar { get; set; }
        public double Apr { get; set; }
        public double Mai { get; set; }
        public double Jun { get; set; }
        public double Jul { get; set; }
        public double Aug { get; set; }
        public double Sep { get; set; }
        public double Okt { get; set; }
        public double Nov { get; set; }
        public double Des { get; set; }

        public double HalfSum { get; set; }
        public double TotSum { get; set; }

        public int Date { get; set; }
        

        public int EId { get; set; }
        public string EName { get; set; }

        public double Sick { get; set; }
        public double SickChild { get; set; }
        public double SickLeave { get; set; }

    }
}
