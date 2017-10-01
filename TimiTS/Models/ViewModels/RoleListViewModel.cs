using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models.ViewModels
{
    public class RoleListViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int NumberOfUsers { get; set; }
    }
}
