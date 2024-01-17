using EnglishService.Models;
using EnglishService.Services.IServices;
using EnglishService.Utities;
using EnglishService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnglishService.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IRegionService _regionService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public CustomerController(ICustomerService customerService, IRegionService regionService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _customerService = customerService;
            _regionService = regionService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Create()
        {
            var viewModel = new CustomerCreateModel
            {
                RegionItems = _regionService.GetAllAsKeyValuePairs()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateModel input)
        {
            if (!ModelState.IsValid)
            {
                input.RegionItems = _regionService.GetAllAsKeyValuePairs();
                return View(input);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);

            input.UserId = userId;

            await _customerService.CreateAsync(input, input.UserId);

            await _userManager.AddToRoleAsync(user, GlobalConstants.CustomerRoleName);

            await _signInManager.RefreshSignInAsync(user);

            return Redirect("/");
        }
    }
}
