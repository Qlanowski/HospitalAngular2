namespace NewWeb.Models
{
    public class DoctorPatient
    {
        public int PatientId { get; set; }
        public string Id { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}