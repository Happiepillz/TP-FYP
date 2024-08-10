using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Models
{
    public class TrainingInstance
    {
        public Guid TrainingInstanceID { get; set; }
        public DateTime TrainingInstanceDateTime { get; set; }
        public Guid TrainingScenarioID { get; set; }

        // Foreign Key 
        public Guid UserID { get; set; }
        // Navigation property 
        public User user { get; set; }


    }
}

