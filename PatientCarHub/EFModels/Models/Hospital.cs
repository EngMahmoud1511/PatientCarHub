using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCarHub.EFModels.Models
{
    public class Hospital
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Specialization { get; set; }
        public int? Rate { get; set; }


        public ICollection<DoctorHospital>? Doctors { get; set; }
        public ICollection<PatientHospital>? Patiens { get; set; }
        public ICollection<Appointments>? Appointments { get; set; }




    }
}
