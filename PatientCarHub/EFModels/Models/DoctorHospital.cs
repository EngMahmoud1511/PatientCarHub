using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCarHub.EFModels.Models
{
    [PrimaryKey(nameof(DoctorId), nameof(HospitalId))]
    public class DoctorHospital
    {


        [ForeignKey("Doctor")]
        public string? DoctorId { get; set; }

        public Doctor? Doctor { get; set; }

        [ForeignKey("Hospital")]
        public string? HospitalId { get; set; }
        public Hospital? Hospital { get; set; }
    }
}
