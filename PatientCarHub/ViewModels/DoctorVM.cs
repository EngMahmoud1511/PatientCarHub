using System.ComponentModel.DataAnnotations;

namespace PatientCarHub.ViewModels
{
    public class DoctorVM
    {
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Specialization { get; set; } 
        public string? FullName { get; set; }
        public string? PicturePaths { get; set; }
        [Required]
        [MaxLength(14)]
        public string? NationalId { get; set; }
        public string? Role { get; set; }
    }
}
