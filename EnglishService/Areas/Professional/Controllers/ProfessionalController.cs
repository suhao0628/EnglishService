using EnglishService.Models;
using EnglishService.Services.IServices;
using EnglishService.Utities;
using EnglishService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EnglishService.Areas.Professional.Controllers
{

    [Authorize(Roles = GlobalConstants.ProfessionalRoleName)]
    [Area("Professional")]
    public class ProfessionalController : Controller
    {
        private readonly IProfessionalService _professionalService;
        private readonly ISpecializationService _specializationService;
        private readonly IRegionService _regionService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public ProfessionalController(IProfessionalService professionalService, ISpecializationService specializationService, IRegionService regionService, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _professionalService = professionalService;
            _specializationService = specializationService;
            _regionService = regionService;
            _userManager = userManager;
            _environment = environment;
        }

        [Authorize]
        public async Task<IActionResult> Update()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            var professionalId = user.ProfessionalId;

            var viewModel = _professionalService.GetProfessionalForUpdate(professionalId.Value);

            viewModel.Id = professionalId.Value;
            viewModel.RegionItems = _regionService.GetAllAsKeyValuePairs();
            viewModel.SpecializationItems = _specializationService.GetAllAsKeyValuePairs();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(ProfessionalEditVM input)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = _professionalService.GetProfessionalForUpdate(input.Id);

                viewModel.Id = input.Id;
                viewModel.RegionItems = _regionService.GetAllAsKeyValuePairs();
                viewModel.SpecializationItems = _specializationService.GetAllAsKeyValuePairs();
                return View(viewModel);
            }

            try
            {
                await _professionalService.UpdateAsync(input, _environment.WebRootPath);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var viewModel = _professionalService.GetProfessionalForUpdate(input.Id);

                viewModel.Id = input.Id;
                viewModel.RegionItems = _regionService.GetAllAsKeyValuePairs();
                viewModel.SpecializationItems = _specializationService.GetAllAsKeyValuePairs();
                return View(viewModel);
            }

            return Redirect("/");
        }
    }
}
