namespace PatientCarHub.ViewModels
{
    public class UserDoctorVM:UserVM
    {
        public IFormFile IdPath { get; set; }
        public string? IdentifierPath { get; set; }
        public bool IsAccountAcsepted { get; set; } = false;


    }
}
