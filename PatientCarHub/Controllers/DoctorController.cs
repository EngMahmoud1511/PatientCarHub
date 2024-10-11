using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PatientCarHub.EFModels.Models;
using PatientCarHub.Repositories.IRepositories;
using PatientCarHub.ViewModels;
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
        public IActionResult Home()
        {
            return View();
        }
        public async Task<IActionResult> SearchForPatient(string nationalId)
        {
            var patient=await _userRepository.FindPatientByNationalId(nationalId);
            return View(patient);
        }

        public async Task<IActionResult> SearchForPatientHistory(string patientId)
        {
            var patient = await _unitOfWork.Examens.FindAll(
                e=>e.PatientId==patientId,new string[] { "StaticFiles" });
            return View(patient);
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
            return View();
        }



    }
}
