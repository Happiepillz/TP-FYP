using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPProject.Models
{
    public class ActivityLog
    {
        public Guid ActivityLogID { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public string ActivityDataName { get; set; }
        public string ActivityDataValue { get; set; }
        public string ActivityStatus { get; set; }

        // foreign key
        public Guid UserId { get; set; }
        public Guid TrainingInstanceID { get; set; }
        public Guid TSStageID { get; set; }
        public Guid ActivityTypeID { get; set; }
        public Guid DrugID { get; set; }


        // Navigation Property
        public User User { get; set; }
        public Drug Drug { get; set; }
        public TrainingSceneStage TSStage { get; set; }
        public ActivityType ActivityType { get; set; }
        public TrainingInstance trainingInstance { get; set; }

    }
}
