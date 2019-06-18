using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Models.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string TelNo { get; set; }

        public string Position { get; set; }

        public string DepartmentTitle { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentCode { get; set; }
    }
}
