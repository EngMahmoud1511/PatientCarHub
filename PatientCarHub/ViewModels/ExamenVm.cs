using PatientCarHub.EFModels.Models;
using System.ComponentModel.DataAnnotations;

namespace PatientCarHub.ViewModels
{
    public class ExamenVm
    {
        public string? PatientId { get; set; }
        public string? DoctorId { get; set; }
        public string ExamenName { get; set; }
        public DateTime? ExamenDate { get; set; } = DateTime.Now;
    }
}
