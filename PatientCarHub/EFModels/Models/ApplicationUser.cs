using Microsoft.AspNetCore.Identity;

namespace PatientCarHub.EFModels.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAccountAcsepted { get; set; } = true;


    }
}
