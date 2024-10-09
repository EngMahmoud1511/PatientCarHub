using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientCarHub.EFModels.Models;
using PatientCarHub.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatientCarHub.Repositories.IRepositories
{
    public class UserRepository
    {
        private readonly  UserManager<ApplicationUser> _UserManeger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration _configuration;

        public UserRepository(UserManager<ApplicationUser> patient, 
            IMapper mapper,IUnitOfWork unitOfWork,IConfiguration configuration)
            
        {
            _UserManeger = patient;
            _mapper = mapper; 
            _configuration = configuration;
            this.unitOfWork = unitOfWork;
        }
       
        public async Task<string> Login(string email,string password)
        {
            var user = await _UserManeger.FindByEmailAsync(email);
            if (user != null && await _UserManeger.CheckPasswordAsync(user, password)==true) 
            {
                var role=await _UserManeger.GetRolesAsync(user);  
                
               string token= GenerateJwtToken(user,role.FirstOrDefault());
                return token;
            }
            else
            {
               return null;
            } 
        }
        public async Task<List<UserVM>> FindDoctorBySpecialization(string Specialization) 
        {
            string role = "Doctor";
            var doctors = await _UserManeger.GetUsersInRoleAsync(role);
            var doctorsdata =await unitOfWork.Doctors.GetAll();
            List<UserVM> result = new List<UserVM>(); 

            foreach (var item in doctorsdata)
            {
                if (item.Specialization == Specialization)
                {
                    var firstMap = _mapper.Map<UserVM>(doctors.FirstOrDefault(x => x.Id == item.Id));

                    var Uservm = _mapper.Map(item,firstMap);
                    
                }
            }
            return result;
        }
        public async Task<PatientVM> FindPatientByNationalId(string nationalId)
        {
             
            var Patient= await unitOfWork.Patients.Get(x => x.NationalId == nationalId);
            var IdentityPatient=await _UserManeger.FindByIdAsync(Patient.Id);
            var firstMap = _mapper.Map<PatientVM>(Patient);
            var result=_mapper.Map(IdentityPatient,firstMap);

            return result;
        }
        public async Task<bool> SoftDelete(string NationalId)
        {
            var patient = await unitOfWork.Patients.Get(x=>x.NationalId== NationalId);

            patient.DeletedAccount = true;
            var result =  unitOfWork.Patients.Update(patient);
            if (result!=null)
            {
                return true;
            }

            return false;
        }
        private string GenerateJwtToken(ApplicationUser login, string role="")
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier,login.Id));
            claims.Add(new Claim(ClaimTypes.Email, login.Email));
            claims.Add(new Claim(ClaimTypes.Role,role));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(jwtSettings["ExpiryInHours"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public Dictionary<string, string> DecodeJwtToken(string token)
        {
            if (token != null)
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSettings = _configuration.GetSection("JwtSettings");

                var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                // Validate the token and extract claims
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                // Extract claims into a dictionary
                var claimsDict = new Dictionary<string, string>();

                foreach (var claim in jwtToken.Claims)
                {
                    claimsDict[claim.Type] = claim.Value;
                }

                return claimsDict;
            }
            else
                return null;
        }


    }
}
