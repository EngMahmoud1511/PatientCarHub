using System.ComponentModel.DataAnnotations;

namespace PatientCarHub.ViewModels
{
    public class UserVM
    {

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "FullName is required")]
        [StringLength(50, ErrorMessage = "FullName cannot be longer than 50 characters")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "NationalId is required")]
        [StringLength(14, ErrorMessage = "NationalId cannot be longer than 14 number")]
        public string? NationalId { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(500, ErrorMessage = "Address cannot be longer than 50 characters")]
       
        public string? Address { get; set; }
    }
}
