using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PatientCarHub.EFModels.Models;
using PatientCarHub.Repositories.IRepositories;
using PatientCarHub.ViewModels;

namespace PatientCarHub.Controllers
{
    public class DoctorController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _patient;
        private readonly RoleManager<IdentityRole> _roleManager;
        UserRepository _userRepository;
        public DoctorController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> patient,
            IMapper mapper, RoleManager<IdentityRole> roleManaer, UserRepository userRepository)
        {
            _mapper=mapper;
            _patient=patient;
            _roleManager=roleManaer;
           _userRepository=userRepository;
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Home()
        {
            return View();
        }
        public async Task<IActionResult> SearchForPatient(string nationalId)
        {
            var patient=await _userRepository.FindPatientByNationalId(nationalId);
            return View(patient);
        }
        public async Task<IActionResult> registerAsync()
        {
            StaticFiles doctorPatient = new StaticFiles()
            {
                FileName="Career",
                FilePath="Career.pdf"
              
            };
          var ok=await _unitOfWork.StaticFiles.Add(doctorPatient);
            return Content("ok.ToString()");
        }
        
           
    }
}
