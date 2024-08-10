using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Models
{
    public class ActivityCategory
    {
        public Guid ActivityCategoryID { get; set; }
        public String ActivityCategoryName { get; set; }
        public String ActivityCategoryDescription  { get; set; }
    }
}
