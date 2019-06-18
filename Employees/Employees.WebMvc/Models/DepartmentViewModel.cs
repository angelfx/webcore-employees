using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Employees.WebMvc.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter a code")]
        public string Code { get; set; }

        public List<EmployeeViewModel> Employees { get; set; }
    }
}
