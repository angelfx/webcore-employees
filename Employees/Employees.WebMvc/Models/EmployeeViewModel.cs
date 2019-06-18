using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Employees.WebMvc.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter a telephone number")]
        public string TelNo { get; set; }

        [Required(ErrorMessage = "Enter a position")]
        public string Position { get; set; }

        public string DepartmentTitle { get; set; }

        [Required(ErrorMessage = "Select a department")]
        public int? DepartmentId { get; set; }

        public string DepartmentCode { get; set; }

        /// <summary>
        /// Для вывода на форме списка выбора
        /// </summary>
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList DepartmentsList { get; set; }
    }
}
