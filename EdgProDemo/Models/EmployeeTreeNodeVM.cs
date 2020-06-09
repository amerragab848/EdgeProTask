using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgProDemo.Models
{
    public class EmployeeTreeNodeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ManagerId { get; set; }
    }
}
