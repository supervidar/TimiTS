using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models.ViewModels
{
    public class ProjectAndWorkViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<ProjectWorkViewModel> ProjectWork { get; set; }
    }
}
