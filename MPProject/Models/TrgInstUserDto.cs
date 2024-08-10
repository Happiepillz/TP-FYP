using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPProject.Models
{
    public class TrgInstUserDto
    {
        public string UserName { get; set; }
        public Guid TrainingInstanceID { get; set; }
        public DateTime TrainingInstanceDateTime { get; set; }
    }
}
