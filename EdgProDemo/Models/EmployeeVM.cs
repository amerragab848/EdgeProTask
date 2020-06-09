using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EdgProDemo.Models
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SSN { get; set; }
        [Display(Name ="Manager")]
        public int ManagerId { get; set; }
        public List<SelectListItem> Employees
        {
            get;
            set;
        } = new List<SelectListItem>();
    }
}
