using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Models
{
    public class TrainingScene
    {
        public Guid TrainingSceneID { get; set; }
        public string TrainingSceneName { get; set; }
        public string TrainingSceneDescription { get; set; }
        public Guid PatientID { get; set; }
        public Guid TrainingScenarioID { get; set; }
    }
}
