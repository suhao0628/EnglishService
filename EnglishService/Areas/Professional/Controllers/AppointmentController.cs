using EnglishService.Services.IServices;
using EnglishService.Utities;
using EnglishService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnglishService.Areas.Professional.Controllers
{
    [Authorize(Roles = GlobalConstants.ProfessionalRoleName)]
    [Area("Professional")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IProfessionalService _professionalService;

        public AppointmentController(IAppointmentService appointmentService, IProfessionalService professionalService)
        {
            _appointmentService = appointmentService;
            _professionalService = professionalService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var professionalId = _professionalService.GetProfessionalIdByUserId(userId);

            var viewModel = new AppointmentsListCustomerVM
            {
                Appointments = await _appointmentService.GetUpcomingAsync(professionalId.Value),
                
                PastAppointments = await _appointmentService.GetPastAsync(professionalId.Value)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmAppointment(int appointmentId)
        {
            await _appointmentService.ConfirmAsync(appointmentId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeclineAppointment(int appointmentId)
        {
            await _appointmentService.DeclineAsync(appointmentId);
            return RedirectToAction(nameof(Index));
        }
    }
}
