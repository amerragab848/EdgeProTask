using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public int SSN { get; set; }
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }
        public virtual ICollection<Employee> EmpManager { get; set; }
        public virtual ICollection<EmployeeData> EmployeeDatas { get; set; }


    }
}
