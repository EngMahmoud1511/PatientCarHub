namespace PatientCarHub.ViewModels
{
    public class UserDoctorVM:UserVM
    {
        public IFormFile IdPath { get; set; }
        public IFormFile PicturePath { get; set; }
        public string? IdentifierPath { get; set; }
        public string? PicturePaths { get; set; }
        public string? Specialization { get; set; }

        public bool IsAccountAcsepted { get; set; } = false;



    }
}
