using AutoMapper;
using EnglishService.Attributes;
using EnglishService.Models;
using EnglishService.Utities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace EnglishService.ViewModels
{
    public class ProfessionalEditVM
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        [Display(Name = "New Image")]
        [DataType(DataType.Upload)]
        // [ValidateImage(ErrorMessage = "Please select a JPG, JPEG or PNG image")]
        [ValidateNever]
        public IFormFile Image { get; set; }

        [Range(18, 99)]
        public int Age { get; set; }

        [MinLength(6)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Experience (in years)")]
        public int? Experience { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Address { get; set; }

        public string Resume { get; set; }

        [Display(Name = "Region")]
        public int RegionId { get; set; }
        [ValidateNever]
        public IEnumerable<KeyValuePair<string, string>> RegionItems { get; set; }

        [Display(Name = "Specialization")]
        public int SpecializationId { get; set; }
        [ValidateNever]
        public IEnumerable<KeyValuePair<string, string>> SpecializationItems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Professional, ProfessionalEditVM>()
                .ForMember(vm => vm.ImageUrl, opt =>
                    opt.MapFrom(d =>
                        d.Images.FirstOrDefault().ImageUrl != null
                            ? d.Images.FirstOrDefault().ImageUrl
                            : "/img/professionals/" + d.Images.FirstOrDefault().Id + "." +
                              d.Images.FirstOrDefault().Extension));
        }
    }
}
