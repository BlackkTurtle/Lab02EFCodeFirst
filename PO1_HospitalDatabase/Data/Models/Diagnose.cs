namespace PO1_HospitalDatabase.Data.Models
{
    public class Diagnose
    {
        public int DiagnoseId { get; set; }
        public string DiagnoseName { get; set; }
        public string Comments { get; set; }
        public int PatientId { get; set; }
        public virtual Patients Patient { get; set; } = null!;
    }
}
