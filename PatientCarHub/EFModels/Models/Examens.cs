namespace PatientCarHub.EFModels.Models
{
    public class Examens
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? PatientId { get; set; }

        public Patient? Patient { get; set; }  
        public string? DoctorId { get; set; }
        public Doctor? Doctor { get; set; } 
        public string? ExamenName { get; set; }
        public DateTime? ExamenDate { get; set; } 

        public string? StaticFilesId { get; set; }
        public StaticFiles? StaticFiles { get; set; }







    }
}
