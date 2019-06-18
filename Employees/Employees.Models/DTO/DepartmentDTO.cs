using System.Collections.Generic;

namespace Employees.Models.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public List<EmployeeDTO> Employees {get;set;}
    }
}
