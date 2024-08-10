using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPProject.Models
{
    public class Patient
    {
        public Guid PatientID { get; set; }
        public string PatientNRIC { get; set; }
        public string PatientName { get; set; }
        public char Gender { get; set; }
        public string Status { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Race { get; set; }
        public Decimal Height { get; set; }
        public Decimal Weight { get; set; }
    }
}
