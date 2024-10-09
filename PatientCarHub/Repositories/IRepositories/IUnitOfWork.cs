using PatientCarHub.EFModels.Models;
using System;

namespace PatientCarHub.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {


      

        IBaseRepository<DoctorHospital> DoctorHospitals { get; } 
        IBaseRepository<Appointments> Appointments { get; } 
        IBaseRepository<DoctorPatient> DoctorPatients { get; }
        IBaseRepository<DoctorPatient> HealthFollowUps { get; }
        IBaseRepository<Hospital> Hospitals { get; } 
        IBaseRepository<Doctor> Doctors { get; } 
        IBaseRepository<Patient> Patients { get; } 

        IBaseRepository<StaticFiles> StaticFiles { get; }
        IBaseRepository<Examens> Examens { get; }


        IBaseRepository<PatientHospital> PatientHospitals { get; }

        void Save();

    }
}
