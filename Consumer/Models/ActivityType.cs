using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Models
{
    public class ActivityType
    {

        public Guid ActivityTypeID { get; set; }
        public String ActivityTypeName { get; set; }
        public String ActivityTypeDescription { get; set; }
        public Guid ActivityCategoryID { get; set; }

    }
}
