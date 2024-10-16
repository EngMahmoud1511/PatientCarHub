using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PatientCarHub.EFModels.Models;
using PatientCarHub.ViewModels;

using PatientCarHub.Repositories;
using PatientCarHub.Repositories.IRepositories;
using System.Reflection.Metadata.Ecma335;

using System.Security.Claims;

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

        public async Task<IActionResult> Index()

        {
            string? token = HttpContext.Session.GetString("token");

            if (token == null)
                return RedirectToAction("Login", "Home");

            
            var currentUserData = _userRepository.DecodeJwtToken(token);
            if (currentUserData[ClaimTypes.Role]!="Doctor")
                return View("Error");

            var currentUserId = currentUserData[ClaimTypes.NameIdentifier];

            var currentDoctor =await _unitOfWork.Doctors.Get(e => e.Id == currentUserId);
            var result = _mapper.Map<DoctorVM>(currentDoctor);
            return View(result);
        }

        public async Task<IActionResult> SearchForPatient(string? nationalId)

        {
            if (nationalId == null)
                nationalId = TempData["nationalId"]?.ToString();
            var patient=await _userRepository.FindPatientByNationalId(nationalId);
            return View("PatientDetails", patient);
        }

        public async Task<IActionResult> SearchForPatientHistory(string patientId)
        {
            var patient = await _unitOfWork.Examens.FindAll(

                e=>e.PatientId==patientId,new string[] { "StaticFiles" ,"Doctor"});

            return View("PatientHistory",patient);

        }

        public IActionResult assignAnExamen(string patientId)
        {
            ExamenVm examenVm = new ExamenVm();
            examenVm.PatientId = patientId;
            
            return View(examenVm);
        }
        [HttpPost]
        public async Task<IActionResult> assignAnExamen( ExamenVm examenVM)
        {
            if(!ModelState.IsValid)
                return View(examenVM);

            string token= HttpContext.Session.GetString("token");
            var userData=_userRepository.DecodeJwtToken(token);
            examenVM.DoctorId=userData[ClaimTypes.NameIdentifier];
            var examne = _mapper.Map<Examens>(examenVM);
            await _unitOfWork.Examens.Add(examne);
            _unitOfWork.Save();
            var nationalId=(await _unitOfWork.Patients.Get(p=>p.Id==examenVM.PatientId)).NationalId;
            TempData["nationalId"] = nationalId;
            return RedirectToAction("SearchForPatient", "Doctor");
        }


    }
}
