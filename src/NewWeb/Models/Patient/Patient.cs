using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Models
{
    public enum Sex
    {
        Male,
        Female
    }

    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Sex Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string PESEL { get; set; }
        public string Email { get; set; }


        public virtual ICollection<DoctorPatient> DoctorPatients { get; set; }
       
    }
}
