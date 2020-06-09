using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public int SSN { get; set; }
        public int? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public Employee Manager { get; set; }
        public virtual ICollection<Employee> EmpManager { get; set; }
        public virtual ICollection<EmployeeData> EmployeeDatas { get; set; }


    }
}
