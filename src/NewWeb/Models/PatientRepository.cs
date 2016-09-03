using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Models
{
    public class PatientRepository : IPatientRepository
    {
        private MedContext _context;
        private ILogger<PatientRepository> _logger;

        public PatientRepository(MedContext context, ILogger<PatientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }   

        public void DoctorAddPatient(Patient newPatient, string name)
        {
            var doctor = _context.Doctors.Where(d => d.UserName == name).FirstOrDefault();
            _context.Add(newPatient);
            _context.DoctorPatients.Add(new DoctorPatient() { Id = doctor.Id, PatientId = newPatient.PatientId });
        }

        public IEnumerable<Patient> GetDoctorsPatients(string name)
        {
            var patients = _context.Doctors
                .Join(
                _context.DoctorPatients,
                d => d.Id,
                dp => dp.Id,
                (d, dp) => new { d, dp }
                )
                .Join(
                _context.Patients,
                pdp => pdp.dp.PatientId,
                pa => pa.PatientId,
                (pdp, pa) => new { pdp, pa }
                )
                .Where(c => c.pdp.d.UserName == name)
                .Select(c => c.pa)
                .ToList();

            return patients;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
