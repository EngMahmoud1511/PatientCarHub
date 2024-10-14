using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCarHub.EFModels.Models
{
    public class StaticFiles
    {
        public string Id {  get; set; }=Guid.NewGuid().ToString();
        [NotMapped]
        public IFormFile File { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; } 
        public DateTime? UploadeDate { get; set; } 
        
    }
}
