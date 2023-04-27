using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO1_HospitalDatabase.Data.Models
{
    public class Medicaments
    {
        public int MedicamentId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PatientMedicament> PatientMedicaments { get; } = new List<PatientMedicament>();
    }
}
