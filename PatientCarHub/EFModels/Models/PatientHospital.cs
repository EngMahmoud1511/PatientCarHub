using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PatientCarHub.EFModels.Models
{
    [PrimaryKey(nameof(HospitalId), nameof(PatientId), nameof(FollowUpDate))]
    public class PatientHospital
    {

        [ForeignKey("Hospital")]
        public string? HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        [ForeignKey("Patient")]
        public string? PatientId { get; set; }
        public string? DoctorId { get; set; }

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }

        [Required]
        public DateTime DateAssigned { get; set; }
        public DateTime FollowUpDate { get; set; } = DateTime.Now;

        public DateTime? NextFollowUpDate { get; set; }

        [MaxLength(500)]
        public string? HealthStatus { get; set; }

        [MaxLength(1000)]
        public string? TreatmentPlan { get; set; }

        [MaxLength(1000)]
        public string? Medications { get; set; } // الأدوية

        [MaxLength(2000)]
        public string? MedicalTests { get; set; } // التحاليل

        [MaxLength(2000)]
        public string? RadiologyReports { get; set; } // الأشعة

        [MaxLength(1000)]
        public string? DoctorNotes { get; set; }
        [Required]
        public bool HasExamine { get; set; } = false;
    }
}
