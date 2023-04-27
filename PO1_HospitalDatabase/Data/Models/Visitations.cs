namespace PO1_HospitalDatabase.Data.Models
{
    public class Visitations
    {
        public int VisitationId { get; set; }
        public string Comments { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Patients Patient { get; set; } = null!;
    }
}
