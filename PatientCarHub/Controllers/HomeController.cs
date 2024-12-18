using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatientCarHub.EFModels.Models;
using PatientCarHub.Repositories.IRepositories;
using PatientCarHub.ViewModels;
using System.Diagnostics;
using System.Security.Claims;


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
        private readonly IWebHostEnvironment _host;

        public HomeController(ILogger<HomeController> logger, UserRepository userRepository,
            IMapper mapper, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork,
             RoleManager<IdentityRole> roleManager, IWebHostEnvironment host)
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
                HttpContext.Session.SetString("token", token);

                if (roles.First() == "Patient")
                {
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
        public IActionResult PatientRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PatientRegister(UserVM User)
        {
            var nationalIdExist = await unitOfWork.Patients.Get(x => x.NationalId == User.NationalId);
            var emailExist = await _userManager.FindByEmailAsync(User.Email);
            if (nationalIdExist == null && emailExist == null)
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
            string? token = HttpContext.Session.GetString("token");

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
        public async Task<IActionResult>DoctorRegister(UserDoctorVM User)
        {
            if (!ModelState.IsValid || User.IdPath == null || User.PicturePath == null)
            {
                return View(User);
            }

            var nationalIdExist = await unitOfWork.Doctors.Get(x => x.NationalId == User.NationalId);
            var emailExist = await _userManager.FindByEmailAsync(User.Email);

            if (nationalIdExist == null && emailExist == null)
            {
                var user = mapper.Map<ApplicationUser>(User);
                var result = await _userManager.CreateAsync(user, User.Password);

                if (result.Succeeded)
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "DoctorIdentifiers");
                    string identifierFileName = $"{Guid.NewGuid()}_{Path.GetFileName(User.IdPath.FileName)}"; // Generate a unique file name for ID
                    string fullIdentifierPath = Path.Combine(myUpload, identifierFileName);
                    using (var stream = new FileStream(fullIdentifierPath, FileMode.Create))
                    {
                        await User.IdPath.CopyToAsync(stream); // Asynchronous file copy
                    }
                    User.IdentifierPath = identifierFileName;

                    string pictureFileName = $"{Guid.NewGuid()}_{Path.GetFileName(User.PicturePath.FileName)}"; // Generate a unique file name for Picture
                    string fullPicturePath = Path.Combine(myUpload, pictureFileName);
                    using (var stream = new FileStream(fullPicturePath, FileMode.Create))
                    {
                        await User.PicturePath.CopyToAsync(stream); // Asynchronous file copy
                    }
                    User.PicturePaths = pictureFileName;

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
                    }
                    return View(User);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "National ID or Email used before");
            }

            return RedirectToAction("Index");
        }


    }
}
