using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPProject.Models
{
    public class ActivityLogUserDTO
    {
        public Guid Id { get; set; }
        //public Guid ActivityUserID { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public string ActivityDataName { get; set; }
        public string ActivityDataValue { get; set; }
        public string ActivityStatus { get; set; }
        public string UserName { get; set; }
        public string DrugName { get; set; }
        public string TSStageName { get; set; }
        public string ActivityTypeName { get; set; }
        public Guid TrainingInstanceID { get; set; }
        public DateTime TrainingInstanceDateTime { get; set; }

    }
}
