using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EnglishService.ViewModels
{
    public class CustomerCreateModel
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [Range(1, 99)]
        public int Age { get; set; }

        public string Gender { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Select a Region.")]
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        [ValidateNever]
        public IEnumerable<KeyValuePair<string, string>> RegionItems { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
    }
}
