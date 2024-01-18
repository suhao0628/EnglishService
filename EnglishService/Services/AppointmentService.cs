using AutoMapper;
using EnglishService.Data;
using EnglishService.Models;
using EnglishService.Repository;
using EnglishService.Services.IServices;
using EnglishService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnglishService.Services
{
    public class AppointmentService: IAppointmentService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRepository<Rating> _ratingRepository;
        private readonly IRepository<Appointment> _repository;


        private readonly IMapper _mapper;
        public AppointmentService(AppDbContext appDbContext, IRepository<Rating> ratingRepository, IMapper mapper, IRepository<Appointment> repository)
        {
            _appDbContext = appDbContext;
            _ratingRepository = ratingRepository;
            _mapper = mapper;
            _repository = repository;
        }

        public int? GetProfessionalIdByAppointmentId(int appointmentId) => _appDbContext.Professionals.FirstOrDefault(d => d.Appointments.Any(a => a.Id == appointmentId))?.Id;

        public async Task<IEnumerable<AppointmentProfessionalVM>> GetPastByCustomerAsync(int customerId)
        {
            var appointments =
                await _appDbContext.Appointments
                .Include(a => a.Professional)
                    .Where(a => a.CustomerId == customerId
                                && a.DateTime.Date < DateTime.UtcNow.Date)
                    .OrderBy(a => a.DateTime)
                    .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentProfessionalVM>>(appointments);
        }

        public async Task<IEnumerable<AppointmentProfessionalVM>> GetUpcomingByCustomerAsync(int customerId)
        {
            var appointments =
                await _appDbContext.Appointments
                .Include(a => a.Professional)
                    .Where(a => a.CustomerId == customerId
                                && a.DateTime.Date > DateTime.UtcNow.Date)
                    .OrderBy(a => a.DateTime)
                    .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentProfessionalVM>>(appointments);
        }

        public async Task<IEnumerable<AppointmentsCustomerVM>> GetPastAsync(int professionalId)
        {
            var appointments =
                await _appDbContext.Appointments
                .Include(a => a.Customer)
                    .Where(a => a.ProfessionalId == professionalId
                                && a.DateTime.Date < DateTime.UtcNow.Date)
                    .OrderBy(a => a.DateTime)
                    .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentsCustomerVM>>(appointments);
        }

        public async Task<IEnumerable<AppointmentsCustomerVM>> GetUpcomingAsync(int professionalId)
        {
            var appointments =
                await _appDbContext.Appointments
                .Include(a => a.Customer)
                    .Where(a => a.ProfessionalId == professionalId
                                && a.DateTime.Date > DateTime.UtcNow.Date)
                    .OrderBy(a => a.DateTime)
                    .ToListAsync();
            
            return _mapper.Map<IEnumerable<AppointmentsCustomerVM>>(appointments);
        }


        public async Task<bool> AddAsync(int professionalId, int customerId, DateTime date)
        {
            var professionalAppointment = _appDbContext.Professionals
                .FirstOrDefault(d => d.Id == professionalId && d.Appointments.Any(a => a.DateTime == date));

            if (professionalAppointment != null)
            {
                return false;
            }

            await _appDbContext.Appointments.AddAsync(new Appointment
            {
                DateTime = date,
                ProfessionalId = professionalId,
                CustomerId = customerId
            });

            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int appointmentId)
        {
            var appointment =
                await _appDbContext.Appointments
                    .Where(x => x.Id == appointmentId)
                    .FirstOrDefaultAsync();
            _appDbContext.Remove(appointment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ConfirmAsync(int appointmentId)
        {
            var appointment =
                await _appDbContext.Appointments
                    .Where(x => x.Id == appointmentId)
                    .FirstOrDefaultAsync();
            appointment.IsConfirmed = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeclineAsync(int appointmentId)
        {
            var appointment =
                await _appDbContext.Appointments
                    .Where(x => x.Id == appointmentId)
                    .FirstOrDefaultAsync();
            appointment.IsConfirmed = false;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Appointment> GetByUserIdAsync(string userId, int appointmentId)
        {
            return await _appDbContext.Appointments.Where(a => a.Customer.User.Id == userId && a.Id == appointmentId).FirstOrDefaultAsync();
        }

        public async Task<AppointmentProfessionalVM> GetByIdAsync(int id)
        {
            var appointment= await _appDbContext.Appointments.Where(x => x.Id == id).Include(a => a.Professional).FirstOrDefaultAsync();
            return _mapper.Map<AppointmentProfessionalVM>(appointment);
        }
    }
}

