using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientCarHub.EFModels.Models;
using PatientCarHub.ViewModels;
using System;

namespace PatientCarHub.EFModels.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
       
        public DbSet<DoctorHospital> DoctorHospitals { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; } 
        public DbSet<PatientHospital> PatientHospitals { get; set; } 
        public DbSet<Doctor> Doctors { get; set; } 
        public DbSet<Patient> Patients { get; set; } 
       
         

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<StaticFiles> StaticFiles { get; set; }
        public DbSet<Examens> Examens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


        }
    }
}
