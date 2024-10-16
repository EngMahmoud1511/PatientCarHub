namespace PatientCarHub.ViewModels
{
    public class PatientHistoryVM
    {
        public int Id { get; set; } // Maps from 'Examens.Id'

        // Doctor details
        public string DoctorFullName { get; set; } // Maps from 'Doctor.FirstName' + 'Doctor.LastName'
        public string Specialization { get; set; } // Maps from 'Doctor.Specialization'
        public string Addrees { get; set; } // Maps from 'Doctor.Addrees'

        // Static file details
        public string FileName { get; set; } // Maps from 'StaticFiles.FileName'
        public string FilePath { get; set; } // Maps from 'StaticFiles.FilePath'
        public DateTime UploadeDate { get; set; } // Maps from 'StaticFiles.UploadeDate'

        // Examen details
        public string ExamenName { get; set; } // Maps from 'ExamenName'
        public DateTime ExamenDate { get; set; } // Maps from 'ExamenDate'
    }

}
