using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Models
{
    public class DoctorRepository : IDoctorRepository
    {
        private MedContext _context;

        public DoctorRepository(MedContext context)
        {
            _context = context;
        }

        public void AddDoctor(Doctor newDoctor)
        {
            _context.Add(newDoctor);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _context.Doctors.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
