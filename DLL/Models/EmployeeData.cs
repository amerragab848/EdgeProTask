using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class EmployeeData:BaseEntity
    {
        public double Age { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
