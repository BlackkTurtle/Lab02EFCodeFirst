namespace PO1_HospitalDatabase.Data.Models
{
    public class Patients
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public bool HasInsurance { get; set; }
        public virtual ICollection<Diagnose> Diagnoses { get; } = new List<Diagnose>();
        public virtual ICollection<PatientMedicament> PatientMedicaments { get; } = new List<PatientMedicament>();
        public virtual ICollection<Visitations> Visitations { get; } = new List<Visitations>();
    }
}
