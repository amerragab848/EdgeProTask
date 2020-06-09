using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgeProTask.Models
{
    public class TreeNode
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public TreeNode Manager { get; set; }
        public List<TreeNode> Children { get; set; }

    }
}
