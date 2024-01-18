using System.ComponentModel.DataAnnotations;

namespace EnglishService.ViewModels
{
    public class ProfessionalListsVM: PageInfoVM
    {
        [Display(Name = "Search by text")]
        public string SearchItem { get; set; }

        [Display(Name = "Region")]
        public int? RegionId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> RegionItems { get; set; }

        [Display(Name = "Specialization")]
        public int? SpecializationId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SpecializationItems { get; set; }

        public IEnumerable<ProfessionalInfoListVM> Professionals { get; set; }
    }
}
