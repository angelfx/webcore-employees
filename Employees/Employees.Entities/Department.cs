using System;
using System.Collections.Generic;

namespace Employees.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
