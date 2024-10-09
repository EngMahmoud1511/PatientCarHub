using System.ComponentModel.DataAnnotations;

namespace PatientCarHub.EFModels.Models
{
    public class Patient
    {
        [Key]
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; } 
        public string? NationalId { get; set; }
        public bool DeletedAccount { get; set; } = false; 
        public ICollection<DoctorPatient>? Doctors { get; set; }
    }
}
