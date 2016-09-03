using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NewWeb.Models
{
    public class MedContext : IdentityDbContext<Doctor>
    {
        private IConfigurationRoot _config;

        public MedContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DoctorPatient>()
                .HasKey(t => new { t.Id, t.PatientId });

            modelBuilder.Entity<DoctorPatient>()
             .HasOne(pt => pt.Patient)
             .WithMany(p => p.DoctorPatients)
             .HasForeignKey(pt => pt.PatientId);

            modelBuilder.Entity<DoctorPatient>()
             .HasOne(pt => pt.Doctor)
             .WithMany(t => t.DoctorPatients)
             .HasForeignKey(pt => pt.Id);
          

            // magiczna linika, pozwala dodać Identity bo inaczej wyskakuje blad z UserLogin
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:MedContextConnection"]);
        }


    }
}

