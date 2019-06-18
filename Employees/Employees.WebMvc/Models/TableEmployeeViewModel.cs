using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.WebMvc.Models
{
    public class TableEmployeeViewModel
    {
        public TableEmployeeViewModel()
        {
            Eomployees = new List<EmployeeViewModel>();

            Search = new EmployeeViewModel();
        }

        public List<EmployeeViewModel> Eomployees { get; set; }

        public EmployeeViewModel Search { get; set; }
    }
}
