using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgProDemo.Models
{
    public class EmployeeDataListingVM
    {
        public int Id { get; set; }
        public double Age { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string EmployeeName { get; set; }
    }
}
