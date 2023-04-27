namespace PO1_HospitalDatabase.Data.Models
{
    public class PatientMedicament
    {
        public int PatientId { get; set; }
        public int MedicamentId { get; set; }
        public virtual Medicaments Medicament { get; set; } = null!;
        public virtual Patients Patient { get; set; } = null!;
    }
}
