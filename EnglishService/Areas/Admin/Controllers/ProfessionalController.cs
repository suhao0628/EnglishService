using EnglishService.Models;
using EnglishService.Services.IServices;
using EnglishService.Utities;
using EnglishService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnglishService.Areas.Admin.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Admin")]
    public class ProfessionalController : Controller
    {
        private readonly IProfessionalService _professionalService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ProfessionalController(IProfessionalService professionalService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _professionalService = professionalService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public IActionResult AppliedProfessional(int id)
        {
            var viewModel = new ProfessionalListsVM
            {
                Professionals = _professionalService.GetAllAppliedProfessionals(id, GlobalConstants.PerPageCount),
                PageNumber = id,
                ItemsPerPage = GlobalConstants.PerPageCount,
                ItemCount = _professionalService.GetAppliedAndNotValidatedProfessionalsCount()
            };

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Verify(int professionalId, int pageNumber)
        {
            var professional = _professionalService.GetProfessionalById(professionalId);

            var user = _userManager.Users.FirstOrDefault(u => u.Id == professional.UserId);

            var checkIfProfessionalExists = await _professionalService.VerifyAsync(professionalId, professional.UserId);

            if (checkIfProfessionalExists == false)
            {
                return new StatusCodeResult(404);
            }

            await _userManager.AddToRoleAsync(user, GlobalConstants.ProfessionalRoleName);

            //await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction(nameof(AppliedProfessional), new { id = pageNumber });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int professionalId, int pageNumber)
        {
            var checkIfProfessionalExists = await _professionalService.DeleteAsync(professionalId);

            if (checkIfProfessionalExists == false)
            {
                return new StatusCodeResult(404);
            }

            return RedirectToAction(nameof(AppliedProfessional), new { id = pageNumber });
        }
    }
}
