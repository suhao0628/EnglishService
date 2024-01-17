using System.ComponentModel.DataAnnotations;

namespace EnglishService.ViewModels
{
    public class RegionCreateModel
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }

        public int? PostCode { get; set; }
    }
}
