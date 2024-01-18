using EnglishService.Models;
using EnglishService.ViewModels;

namespace EnglishService.Services.IServices
{
    public interface IRatingService
    {
        IEnumerable<RatingVM> GetAllRatingsByProfessionalId(int professionalId, int count);
    }

}
