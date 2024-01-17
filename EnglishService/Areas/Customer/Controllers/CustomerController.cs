using EnglishService.Models;
using EnglishService.Services.IServices;
using EnglishService.Utities;
using EnglishService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EnglishService.Areas.Customer.Controllers
{
    [Authorize(Roles = GlobalConstants.CustomerRoleName)]
    [Area("Customer")]
    public class CustomerController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;

        public CustomerController(IRegionService regionService, ICustomerService customerService, UserManager<AppUser> userManager)
        {
            _regionService = regionService;
            _customerService = customerService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Update()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            var customerId = user.CustomerId;

            var viewModel = _customerService.GetCustomerById(customerId.Value);

            viewModel.RegionItems = _regionService.GetAllAsKeyValuePairs();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(CustomerUpdateModel input)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = _customerService.GetCustomerById(input.Id);

                viewModel.RegionItems = _regionService.GetAllAsKeyValuePairs();
                return View(input);
            }

            await _customerService.UpdateAsync(input);

            return Redirect("/");
        }
    }
}
