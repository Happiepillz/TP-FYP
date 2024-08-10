using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPProject.Models
{
    public class Drug
    {
        public Guid DrugId { get; set; }
        public string DrugName { get; set; }
        public string DrugDosage { get; set; }
        public string DrugDescription { get; set; }
        public string DrugType { get; set; }
        public string DrugSideEffects { get; set; }
        public string DrugPrecautions { get; set; }
        public string DrugInteractions { get; set; }
        public string DrugRemarks { get; set; }

    }
}
