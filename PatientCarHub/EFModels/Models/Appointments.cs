namespace PatientCarHub.EFModels.Models
{
    public class Appointments
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public string? DoctorId { get; set; } 
        public string? PatientId { get; set;} 
        public DateTime AppointmentDate { get; set; } 
        public string? HospitalId { get; set; } 
        public string? Phone { get; set; }  
        public Patient? Patient { get; set; } 
        public Doctor? Doctor { get; set; }
        public Hospital? Hospital { get; set; }
    }
}
