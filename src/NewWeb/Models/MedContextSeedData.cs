using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewWeb.Models
{
    public class MedContextSeedData
    {
        private MedContext _context;
        private UserManager<Doctor> _userManager;

        public MedContextSeedData(MedContext context, UserManager<Doctor> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            if(await _userManager.FindByEmailAsync("ulanowskikarol@gmail.com") == null)
            {
                var doctor = new Doctor() { UserName = "Qlanowski", Email = "ulanowskikarol@gmail.com" };
                await _userManager.CreateAsync(doctor, "P@ssw0rd!");
            }

            if (!_context.Patients.Any())
            {

                _context.Patients.AddRange(
                new Patient() { Name = "Kacper", Surname = "Lizak", BirthDate = new DateTime(1961, 4, 12), PESEL = "12345658905", Email = "kliz@wp.pl", Sex = Sex.Male },
                new Patient() { Name = "Ania", Surname = "Krzysztofiak", BirthDate = new DateTime(1970, 5, 5), PESEL = "92345678905", Email = "akrz@wp.pl", Sex = Sex.Female },
                new Patient() { Name = "Kasia", Surname = "Bartczak", BirthDate = new DateTime(1986, 5, 2), PESEL = "12675678905", Email = "kbar@wp.pl", Sex = Sex.Female },
                new Patient() { Name = "Karol", Surname = "Liczny", BirthDate = new DateTime(1990, 2, 24), PESEL = "84345678905", Email = "klic@wp.pl", Sex = Sex.Male }
                );

                await _context.SaveChangesAsync();
            }
            if (!_context.DoctorPatients.Any())
            {
                _context.AddRange(
                    new DoctorPatient() { Id = "32b4a717-9895-414a-926e-172b535f1f9f", PatientId = 1 },
                    new DoctorPatient() { Id = "32b4a717-9895-414a-926e-172b535f1f9f", PatientId = 2 },
                    new DoctorPatient() { Id = "32b4a717-9895-414a-926e-172b535f1f9f", PatientId = 3 },
                    new DoctorPatient() { Id = "42f88770-7e3a-4676-affd-21c6d799a40d", PatientId = 3 },
                    new DoctorPatient() { Id = "42f88770-7e3a-4676-affd-21c6d799a40d", PatientId = 4 }

                    );
                await _context.SaveChangesAsync();
            }
            

        }
    }

            
}
