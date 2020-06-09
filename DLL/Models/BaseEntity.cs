using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class BaseEntity
    {
        public int Id
        {
            get;
            set;
        }
        public DateTime AddedDate
        {
            get;
            set;
        }
        public DateTime ModifiedDate
        {
            get;
            set;
        }
      
    }
}
