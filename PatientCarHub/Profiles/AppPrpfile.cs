using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PatientCarHub.EFModels.Models;
using PatientCarHub.ViewModels;

namespace PatientCarHub.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {


            CreateMap<Doctor, UserVM>().
                ForMember(dest => dest.FullName, src => src.MapFrom(x => x.FirstName + " " + x.LastName))
                .ReverseMap();
            CreateMap<ApplicationUser, UserVM>().ReverseMap();


            CreateMap<Patient, UserVM>().
                ForMember(dest => dest.FullName, src => src.MapFrom(x => x.FirstName + " " + x.LastName))
                .ReverseMap();
               
            CreateMap<UserVM, ApplicationUser>()
             .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore during mapping
               .AfterMap((src, dest) => dest.Id = Guid.NewGuid().ToString())
               .ReverseMap(); // Set new Guid after mapping

           
            CreateMap<Patient, PatientVM>()
              .ForMember(dest => dest.FullName, src => src.MapFrom(x => x.FirstName + " " + x.LastName))
              .ReverseMap();

            // Map ApplicationUser to PatientVM (for user-specific details like UserName, Email)
            CreateMap<ApplicationUser, PatientVM>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))

                .ForMember(dest => dest.Password, opt => opt.Ignore());// Password shouldn't be mapped from database

             CreateMap<Patient, PatientLoginVM>()
              .ForMember(dest => dest.FullName, src => src.MapFrom(x => x.FirstName + " " + x.LastName))
              .ReverseMap();

            // Map ApplicationUser to PatientVM (for user-specific details like UserName, Email)
            CreateMap<ApplicationUser, PatientLoginVM>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.Id))

                .ForMember(dest => dest.Password, opt => opt.Ignore()); // Password shouldn't be mapped from database
                                                                        // Map Doctor to DoctorVM
            CreateMap<Doctor, DoctorVM>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(x => x.FirstName + " " + x.LastName))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization))
                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.NationalId))
                .ReverseMap();

          

        }

    }
}
