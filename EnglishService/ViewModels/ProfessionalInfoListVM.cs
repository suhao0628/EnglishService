using AutoMapper;
using EnglishService.Models;
using System.Numerics;

namespace EnglishService.ViewModels
{
    public class ProfessionalInfoListVM
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public double? AverageRating { get; set; }

        public int? Experience { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Resume { get; set; }

        public string RegionName { get; set; }

        public string SpecializationName { get; set; }

        public bool IsApplied { get; set; }

        public bool IsApproved { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Professional, ProfessionalInfoListVM>()
                .ForMember(vm => vm.ImageUrl, opt =>
                    opt.MapFrom(d =>
                        d.Images.FirstOrDefault().ImageUrl != null
                            ? d.Images.FirstOrDefault().ImageUrl
                            : "/img/doctors/" + d.Images.FirstOrDefault().Id + "." +
                              d.Images.FirstOrDefault().Extension))
                .ForMember(vm => vm.FullName, opt =>
                    opt.MapFrom(d => d.FirstName + " " + d.LastName))
                .ForMember(vm => vm.AverageRating, opt =>
                    opt.MapFrom(d => d.Ratings.Average(r => r.Number)));
        }
    }
}
