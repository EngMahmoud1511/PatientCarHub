using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCarHub.EFModels.Models
{
    [PrimaryKey(nameof(PatientId), nameof(DoctorId), nameof(FollowUpDate))]

    public class DoctorPatient
    {

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
        public string? PatientId { get; set; }
        public string? DoctorId { get; set; }

        public Doctor? Doctor { get; set; }  // Doctor is an AspNetUser
        public Patient? Patient { get; set; } // Patient is an AspNetUser



    }

}
