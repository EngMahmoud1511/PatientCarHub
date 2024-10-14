using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatientCarHub.EFModels.Models;
using PatientCarHub.Repositories.IRepositories;
using PatientCarHub.ViewModels;
using System.Security.Claims;

namespace PatientCarHub.Controllers
{
    public class PatientController : Controller
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserRepository _userRepository; 
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly IMapper _mapper; 


        public PatientController(IUnitOfWork unitOfWork,UserRepository userRepository
            ,UserManager<ApplicationUser> userManager,IMapper  mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository; 
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DoctorAppointmet(string DoctorNationalId)
        {
           string? token= HttpContext.Session.GetString("token");

            if (token == null)
                return RedirectToAction("Login", "Home");

            var doctor =await _unitOfWork.Doctors.Get(x => x.NationalId == DoctorNationalId);
            var doctordata = await _userManager.FindByIdAsync(doctor.Id);
            var firstMap= _mapper.Map<DoctorVM>(doctordata);
            var result = _mapper.Map(doctor,firstMap);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> DoctorAppointmet(Appointments appointment ,string DoctorNationalId)
        {
           string? token=HttpContext.Session.GetString("token");
            var PatientData=_userRepository.DecodeJwtToken(token);
            appointment.PatientId= PatientData[ClaimTypes.NameIdentifier];
            var doctor = await _unitOfWork.Doctors.Get(x=>x.NationalId==DoctorNationalId);
            appointment.DoctorId = doctor.Id;
            await _unitOfWork.Appointments.Add(appointment);
            return View();  
        }
        public IActionResult HospitalAppointment(string serialNumber)
        {
            return View();
        }
        public async Task<IActionResult> DeleteAccount (string NationalId)
        {
            var result =await _userRepository.SoftDelete(NationalId);
     
                if (result==true) 
                {
                    return RedirectToAction("Index","Home");
                }
            
            return View();

        }
        public async Task<IActionResult> UplodeExamenResult(StaticFiles staticFiles)
        {
            var fileModel = _mapper.Map<StaticFiles>(staticFiles);
            await _unitOfWork.StaticFiles.Add(fileModel);
            return View();
        }
    }
}
