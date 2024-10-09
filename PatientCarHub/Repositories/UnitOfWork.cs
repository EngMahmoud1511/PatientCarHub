using PatientCarHub.EFModels.Data;
using PatientCarHub.EFModels.Models;
using PatientCarHub.Repositories.IRepositories;
using System;

namespace PatientCarHub.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

       
        public IBaseRepository<DoctorHospital> DoctorHospitals { get; private set; }
        public IBaseRepository<Appointments> Appointments { get; private set; } 
        public IBaseRepository<DoctorPatient> DoctorPatients { get; private set; }
        public IBaseRepository<DoctorPatient> HealthFollowUps { get; private set; }
        public IBaseRepository<Hospital> Hospitals { get; private set; }
        public IBaseRepository<Examens> Examens { get; private set; }
        public IBaseRepository<StaticFiles> StaticFiles { get; private set; }

        public IBaseRepository<PatientHospital> PatientHospitals { get; private set; }

        public IBaseRepository<Doctor> Doctors {  get; private set; }

        public IBaseRepository<Patient> Patients {  get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
          
            DoctorHospitals = new BaseRepository<DoctorHospital>(_context);
            DoctorPatients = new BaseRepository<DoctorPatient>(_context);
            HealthFollowUps = new BaseRepository<DoctorPatient>(_context);
            Hospitals = new BaseRepository<Hospital>(_context);
            StaticFiles = new BaseRepository<StaticFiles>(_context);
            Examens = new BaseRepository<Examens>(_context);
            Appointments=new BaseRepository<Appointments>(_context);
            PatientHospitals = new BaseRepository<PatientHospital>(_context); 
            Doctors = new BaseRepository<Doctor>(_context); 
            Patients = new BaseRepository<Patient>(_context); 


        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
