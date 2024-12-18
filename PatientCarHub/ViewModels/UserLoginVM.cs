﻿using System.ComponentModel.DataAnnotations;

namespace PatientCarHub.ViewModels
{
    public class UserLoginVM
    {
        public string? Id {  get; set; } 
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

        public string? FullName { get; set; }
        [Required]
        [MaxLength(14)]
        public string? NationalId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Address { get; set; }
        public string? Role { get; set; }
    }
}
