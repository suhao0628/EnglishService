using AutoMapper;
using EnglishService.Models;
using EnglishService.Utities;
using EnglishService.ViewModels;
using System.Configuration;
using System.Numerics;

namespace EnglishService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Professional, ProfessionalInfoListVM>();

            CreateMap<Professional, ProfessionalEditVM>()
                .ForMember(vm => vm.ImageUrl, opt =>
                    opt.MapFrom(d =>
                        d.Images.FirstOrDefault().ImageUrl != null
                            ? d.Images.FirstOrDefault().ImageUrl
                            : "/img/professionals/" + d.Images.FirstOrDefault().Id + "." +
                              d.Images.FirstOrDefault().Extension));


            CreateMap<Professional, ProfessionalInfoListVM>()
                .ForMember(vm => vm.ImageUrl, opt =>
                    opt.MapFrom(d =>
                        d.Images.FirstOrDefault().ImageUrl != null
                            ? d.Images.FirstOrDefault().ImageUrl
                            : "/img/professionals/" + d.Images.FirstOrDefault().Id + "." +
                              d.Images.FirstOrDefault().Extension))
                .ForMember(vm => vm.FullName, opt =>
                    opt.MapFrom(d => d.FirstName + " " + d.LastName));
                //.ForMember(vm => vm.AverageRating, opt =>
                //    opt.MapFrom(d => d.Ratings.Average(r => r.Number)));




            CreateMap<Rating, RatingVM>()
                .ForMember(vm => vm.CustomerFullName, opt =>
                    opt.MapFrom(r => r.Professional.FirstName + " " + r.Professional.LastName))
                .ForMember(vm => vm.CreatedOn, opt =>
                    opt.MapFrom(r => r.CreatedOn.ToString(GlobalConstants.DateTimeFormat)));


            CreateMap<Appointment, AppointmentProfessionalVM>()
                                .ForMember(vm => vm.DateTime, opt =>
                                    opt.MapFrom(a => a.DateTime.ToString(GlobalConstants.DateTimeFormat)))
                                .ForMember(vm => vm.ProfessionalFullName, opt =>
                                    opt.MapFrom(a => a.Professional.FirstName + " " + a.Professional.LastName));

            CreateMap<Appointment, AppointmentsCustomerVM>()
                .ForMember(vm => vm.DateTime, opt =>
                      opt.MapFrom(a => a.DateTime.ToString(GlobalConstants.DateTimeFormat)))
                .ForMember(vm => vm.CustomerFullName, opt =>
                    opt.MapFrom(a => a.Customer.FirstName + " " + a.Customer.LastName))
                .ForMember(vm => vm.Rating, opt =>
                    opt.MapFrom(a => a.Rating.Number))
                .ForMember(vm => vm.RatingComment, opt =>
                    opt.MapFrom(a => a.Rating.Comment));
        }
    }
}
