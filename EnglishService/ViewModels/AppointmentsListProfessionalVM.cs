namespace EnglishService.ViewModels
{
    public class AppointmentsListProfessionalVM
    {
        public IEnumerable<AppointmentProfessionalVM> Appointments { get; set; }

        public IEnumerable<AppointmentProfessionalVM> PastAppointments { get; set; }
    }
}
