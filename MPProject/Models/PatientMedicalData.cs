using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPProject.Models
{
    public class PatientMedicalData
    {
        public Guid PatientMedicalDataID { get; set; }
        public string PatientMedicalDataName { get; set; }
        public string PatientMedicalDataValue { get; set; }
        public DateTime PatientMedicalDataDatetime { get; set; }
        public bool isVitalSign { get; set; }
        public Guid PatientID { get; set; }
    }
}
