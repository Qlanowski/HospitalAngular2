using NewWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.ViewModels
{
    public class PatientViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public Sex Sex { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(11, MinimumLength =11)]
        public string PESEL { get; set; }
        public string Email { get; set; }
    }
}
