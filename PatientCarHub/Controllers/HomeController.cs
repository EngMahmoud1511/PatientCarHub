using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PatientCarHub.EFModels.Models;
using PatientCarHub.Repositories.IRepositories;
using PatientCarHub.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace PatientCarHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHostingEnvironment _host;

        public HomeController(ILogger<HomeController> logger, UserRepository userRepository,
            IMapper mapper, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork,
             RoleManager<IdentityRole> roleManager,IHostingEnvironment host)
        {
            _logger = logger;
            _userRepository = userRepository;
            this.mapper = mapper;
            _userManager = userManager;
            this.unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _host = host;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Loin
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserVM user)
        {
            var userFromDb =await _userManager.FindByEmailAsync(user.Email);

            string token = await _userRepository.Login(user.Email,user.Password);
            
            if (token != null)
            {

                var roles =await _userManager.GetRolesAsync(userFromDb);
                
                if (roles.First() == "Patient")
                {
                    HttpContext.Response.Cookies.Append("token", token);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Doctor");
                }
            }
            ModelState.AddModelError(string.Empty, "Invaled Email Or Password");
            return View(user);
        }
        //Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVM User)
        {
            var nationalIdExist = await unitOfWork.Patients.Get(x => x.NationalId == User.NationalId);
            var emailExist = await _userManager.FindByEmailAsync(User.Email);
            if (nationalIdExist == null && emailExist == null)
            {
                if (User.Role == "Patient")
                {
                    if (nationalIdExist == null && emailExist == null)
                    {
                        var user = mapper.Map<ApplicationUser>(User);

                        var result = await _userManager.CreateAsync(user, User.Password);

                        if (result.Succeeded)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(user, "Patient");
                            var patient = mapper.Map<Patient>(User);
                            patient.Id = user.Id;

                            await unitOfWork.Patients.Add(patient);
                            if (roleResult.Succeeded)
                            {
                                return RedirectToAction("Login");
                            }
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                                return View(User);
                            }
                        }
                    }

                }
                else if (User.Role == "Doctor")
                {
                    if (nationalIdExist == null)
                    {
                        var user = mapper.Map<ApplicationUser>(User);


                        var result = await _userManager.CreateAsync(user, User.Password);

                        if (result.Succeeded)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(user, "Doctor");
                            var Doctor = mapper.Map<Doctor>(User);
                            Doctor.Id = user.Id;

                            await unitOfWork.Doctors.Add(Doctor);

                            if (roleResult.Succeeded)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                                return View(User);
                            }
                        }
                    }

                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "National Id or Email Used befoure");
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult>SearchForDoctor(string Spesialization)
        {
          var doctors=await _userRepository.FindDoctorBySpecialization(Spesialization);
            return View(doctors);
        }
        public async Task<IActionResult>SearchForHospital(string Spesialization)
        {
            string token = HttpContext.Request.Cookies["token"];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }
            var userdata=_userRepository.DecodeJwtToken(token);
            var patientId = userdata[ClaimTypes.NameIdentifier];
            var role = userdata[ClaimTypes.Role];
            var doctors=await unitOfWork.Hospitals.FindAll(criteria:x=>x.Specialization==Spesialization);
            return View(doctors);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult DoctorRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DoctorRegister(UserDoctorVM User)
        {
            if (!ModelState.IsValid || User.IdPath == null)
                return View(User);

            var nationalIdExist = await unitOfWork.Doctors.Get(x => x.NationalId == User.NationalId);
            var emailExist = await _userManager.FindByEmailAsync(User.Email);
            if (nationalIdExist == null && emailExist == null)
            {

                if (nationalIdExist == null && emailExist == null)
                {

                    var user = mapper.Map<ApplicationUser>(User);
                    var result = await _userManager.CreateAsync(user, User.Password);

                    if (result.Succeeded)
                    {

                        string fileName = "";
                        string myUpload = Path.Combine(_host.WebRootPath, "DoctorIdentifiers");
                        fileName = User.IdPath.FileName;
                        string fullPath = Path.Combine(myUpload, fileName);
                        User.IdPath.CopyTo(new FileStream(fullPath, FileMode.Create));
                        User.IdentifierPath = fileName;

                        var roleResult = await _userManager.AddToRoleAsync(user, "Doctor");
                        var doctor = mapper.Map<Doctor>(User);
                        doctor.Id = user.Id;

                        await unitOfWork.Doctors.Add(doctor);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Login");
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                            return View(User);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "National Id or Email Used befoure");
            }

            return RedirectToAction("Index");
        }

    }
}
