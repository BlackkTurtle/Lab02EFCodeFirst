namespace PO1_HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public virtual ICollection<Visitations> Visitations { get; } = new List<Visitations>();
    }
}
