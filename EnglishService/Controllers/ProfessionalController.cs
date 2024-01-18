using EnglishService.Services;
using EnglishService.Services.IServices;
using EnglishService.Utities;
using EnglishService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnglishService.Controllers
{
    [Authorize]
    public class ProfessionalController : Controller
    {

        private readonly IProfessionalService _professionalService;
        private readonly ISpecializationService _specializationService;
        private readonly IRegionService _regionService;
        private readonly IWebHostEnvironment _environment;

        private readonly IRatingService _ratingService;

        public ProfessionalController(IProfessionalService professionalService,ISpecializationService specializationService, IRegionService regionService, IWebHostEnvironment environment, IRatingService ratingService)
        {
            _professionalService = professionalService;
            _specializationService = specializationService;
            _regionService = regionService;
            _environment = environment;
            _ratingService = ratingService;
        }

        [AllowAnonymous]
        public IActionResult Profile(int professionalId)
        {
            
            var viewModel = new ProfessionalProfileVM()
            {
                Ratings = _ratingService.GetAllRatingsByProfessionalId(professionalId, GlobalConstants.PerPageCount),
                Professional = _professionalService.GetProfessionalListById(professionalId)
            };
            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult Index(int id, [FromQuery] string searchItem, int? regionId, int? specializationId)
        {
            if (id <= 0)
            {
                return new StatusCodeResult(400);
            }

            var viewModel = new ProfessionalListsVM
            {
                RegionItems = _regionService.GetAllAsKeyValuePairs(),
                SpecializationItems = _specializationService.GetAllAsKeyValuePairs(),
                PageNumber = id,
                ItemsPerPage = GlobalConstants.PerPageCount,
                SearchItem = searchItem,
                RegionId = regionId,
                SpecializationId = specializationId
            };

            if (searchItem != null || regionId != null || specializationId != null)
            {
                viewModel.Professionals = _professionalService.GetAllValidatedProfessionals(id, GlobalConstants.PerPageCount, searchItem, regionId, specializationId);
            }
            else
            {
                viewModel.Professionals = _professionalService.GetAllValidatedProfessionals(id, GlobalConstants.PerPageCount);
            }

            viewModel.ItemCount = viewModel.Professionals.Count();

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new ProfessionalCreateVM
            {
                RegionItems = _regionService.GetAllAsKeyValuePairs(),
                SpecializationItems = _specializationService.GetAllAsKeyValuePairs()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProfessionalCreateVM input)
        {
            if (!ModelState.IsValid)
            {
                input.RegionItems = _regionService.GetAllAsKeyValuePairs();
                input.SpecializationItems = _specializationService.GetAllAsKeyValuePairs();
                return View(input);
            }

            input.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _professionalService.CreateAsync(input, $"{_environment.WebRootPath}/img");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                input.RegionItems = _regionService.GetAllAsKeyValuePairs();
                input.SpecializationItems = _specializationService.GetAllAsKeyValuePairs();
                return View(input);
            }

            return Redirect("/");
        }
    }
}
