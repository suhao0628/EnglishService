using EnglishService.Models;
using EnglishService.ViewModels;

namespace EnglishService.Services.IServices
{
    public interface IAppointmentService
    {
        int? GetProfessionalIdByAppointmentId(int appointmentId);
        Task<IEnumerable<AppointmentProfessionalVM>> GetPastByCustomerAsync(int customerId);
        Task<IEnumerable<AppointmentProfessionalVM>> GetUpcomingByCustomerAsync(int customerId);

        Task<IEnumerable<AppointmentsCustomerVM>> GetPastAsync(int customerId);
        Task<IEnumerable<AppointmentsCustomerVM>> GetUpcomingAsync(int customerId);

        Task<bool> AddAsync(int professionalId, int customerId, DateTime date);
        Task DeleteAsync(int appointmentId);
        Task ConfirmAsync(int appointmentId);
        Task DeclineAsync(int appointmentId);
        Task<Appointment> GetByUserIdAsync(string userId, int appointmentId);
        Task<AppointmentProfessionalVM> GetByIdAsync(int id);
    }
}
