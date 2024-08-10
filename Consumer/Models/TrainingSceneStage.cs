using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Consumer.Models
{
    public class TrainingSceneStage
    {
        [Key]
        public Guid TSStageID { get; set; }
        public String TSStageName { get; set; }
        public String TSStageDescription { get; set; }
        public Guid TrainingSceneID { get; set; }
    }
}
