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
        }
    }
}
