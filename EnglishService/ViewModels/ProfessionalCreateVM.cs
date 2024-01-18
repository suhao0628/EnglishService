using EnglishService.Attributes;
using EnglishService.Utities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EnglishService.ViewModels
{
    public class ProfessionalCreateVM
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [DataType(DataType.Upload)]
        [Required]
        [ValidateImage(ErrorMessage = "Please select a JPG, JPEG or PNG image")]
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

        [Required(ErrorMessage = "Select a region.")]
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        [ValidateNever]
        public IEnumerable<KeyValuePair<string, string>> RegionItems { get; set; }
        [ValidateNever]
        [Required(ErrorMessage = "Select a specialization.")]
        [Display(Name = "Specialization")]
        public int SpecializationId { get; set; }
        [ValidateNever]
        public IEnumerable<KeyValuePair<string, string>> SpecializationItems { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
    }
}
