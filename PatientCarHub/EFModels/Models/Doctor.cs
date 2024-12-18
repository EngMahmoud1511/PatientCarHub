﻿using System.ComponentModel.DataAnnotations;

namespace PatientCarHub.EFModels.Models
{
    public class Doctor
    {
        [Key]
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Addrees { get; set; }
        public string? Specialization { get; set; }
        public string? NationalId { get; set; }
        public bool IsAccountAcsepted { get; set; } = false;
        public bool DeletedAccount { get; set; } = false; 
        public string? IdentifierPath { get; set; } 
        public string? PicturePaths { get; set; } 

        public ICollection<DoctorPatient>? Patients { get; set;} 
        public ICollection<DoctorHospital>?Hospitals  { get; set;} 
    }
}
