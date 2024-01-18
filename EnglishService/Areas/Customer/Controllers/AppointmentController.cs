using EnglishService.Services;
using EnglishService.Services.IServices;
using EnglishService.Utities;
using EnglishService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnglishService.Areas.Customer.Controllers
{
    [Authorize(Roles = GlobalConstants.CustomerRoleName)]
    [Area("Customer")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDateTimeParserService _dateTimeParserService;
        private readonly ICustomerService _customerService;
        private readonly IRatingService _ratingService;
        private readonly IProfessionalService _professionalService;

        public AppointmentController(IAppointmentService appointmentService, IDateTimeParserService dateTimeParserService, ICustomerService customerService, IRatingService ratingService, IProfessionalService professionalService)
        {
            _appointmentService = appointmentService;
            _dateTimeParserService = dateTimeParserService;
            _customerService = customerService;
            _ratingService = ratingService;
            _professionalService = professionalService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customerId = await _customerService.GetCustomerIdByUserId(userId);

            var viewModel = new AppointmentsListProfessionalVM
            {
                PastAppointments = await _appointmentService.GetPastByCustomerAsync(customerId.Value),
                Appointments = await _appointmentService.GetUpcomingByCustomerAsync(customerId.Value),
            };
            return View(viewModel);
        }

        [Authorize]
        public IActionResult MakeAppointment(int professionalId)
        {
            var viewModel = new MakeAppointmentVM
            {
                ProfessionalId = professionalId
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MakeAppointment(MakeAppointmentVM input)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(MakeAppointment), new { input.ProfessionalId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customerId = await _customerService.GetCustomerIdByUserId(userId);
            if (customerId == null)
            {
                return new StatusCodeResult(404);
            }

            DateTime dateTime;
            try
            {
                dateTime = _dateTimeParserService.ConvertStrings(input.Date, input.Time);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(MakeAppointment), new { input.ProfessionalId });
            }

            return await _appointmentService.AddAsync(input.ProfessionalId, customerId.Value, dateTime) == false ? RedirectToAction("MakeAppointment", new { input.ProfessionalId }) : RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var viewModel = await _appointmentService.GetByIdAsync(id);

            if (viewModel == null)
            {
                return new StatusCodeResult(404);
            }

            return View(viewModel);
        }
    }
}
