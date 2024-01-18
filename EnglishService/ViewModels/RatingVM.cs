using AutoMapper;
using EnglishService.Models;
using EnglishService.Utities;

namespace EnglishService.ViewModels
{
    public class RatingVM: IMapFrom<Rating>
    {
        public string CustomerFullName { get; set; }

        public int Number { get; set; }

        public string Comment { get; set; }

        public string CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Rating, RatingVM>()
                .ForMember(vm => vm.CustomerFullName, opt =>
                    opt.MapFrom(r => r.Professional.FirstName + " " + r.Professional.LastName))
                .ForMember(vm => vm.CreatedOn, opt =>
                    opt.MapFrom(r => r.CreatedOn.ToString(GlobalConstants.DateTimeFormat)));
        }
    }
}
