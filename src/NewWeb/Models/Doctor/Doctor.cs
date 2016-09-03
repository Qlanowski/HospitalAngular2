
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewWeb.Models
{
    public class Doctor :IdentityUser
    {
       
        //public int DoctorId { get; set; }
        public string Ward { get; set; }

        public virtual ICollection<DoctorPatient> DoctorPatients { get; set; }
    }
}