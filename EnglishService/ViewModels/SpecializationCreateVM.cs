using System.ComponentModel.DataAnnotations;

namespace EnglishService.ViewModels
{
    public class SpecializationCreateVM
    {
        [Required]
        [StringLength(225, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(225)]
        public string Description { get; set; }
    }
}
