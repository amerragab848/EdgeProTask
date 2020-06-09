using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EdgProDemo.Models
{
    public class EmployeeDataVM
    {
        public int Id { get; set; }
        public double Age { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<SelectListItem> Employees
        {
            get;
            set;
        } = new List<SelectListItem>();
        [Display(Name ="Employee")]
        public long EmployeeId { get; set; }

    }
}
