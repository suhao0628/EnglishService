using AutoMapper;
using EnglishService.Data;
using EnglishService.Models;
using EnglishService.Repository;
using EnglishService.Services.IServices;
using EnglishService.ViewModels;

namespace EnglishService.Services
{
    public class RatingService: IRatingService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRepository<Rating> _ratingRepository;

        private readonly IMapper _mapper;
        public RatingService(AppDbContext appDbContext, IRepository<Rating> ratingRepository, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public IEnumerable<RatingVM> GetAllRatingsByProfessionalId(int professionalId, int count)
        {
            var model = _appDbContext.Ratings
                .Where(r => r.ProfessionalId == professionalId)
                .OrderByDescending(d => d.Id)
                .Take(count)
                .ToList();

            return _mapper.Map<IEnumerable<RatingVM>>(model);
        }
    }
}
