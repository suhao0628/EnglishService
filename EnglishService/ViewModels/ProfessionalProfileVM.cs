namespace EnglishService.ViewModels
{
    public class ProfessionalProfileVM
    {
        public IEnumerable<RatingVM> Ratings { get; set; }

        public ProfessionalInfoListVM Professional { get; set; }
    }
}
