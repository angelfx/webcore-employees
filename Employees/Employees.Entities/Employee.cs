using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string TelNo { get; set; }

        public string Position { get; set; }

        public virtual Department Department { get; set; }
    }
}
