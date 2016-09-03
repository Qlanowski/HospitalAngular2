using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewWeb.Models
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetAllDoctors();
        void AddDoctor(Doctor newDoctor);
        Task<bool> SaveChangesAsync();
    }
}