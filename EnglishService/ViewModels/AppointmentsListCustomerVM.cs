namespace EnglishService.ViewModels
{
    public class AppointmentsListCustomerVM
    {

        public IEnumerable<AppointmentsCustomerVM> Appointments { get; set; }

        public IEnumerable<AppointmentsCustomerVM> PastAppointments { get; set; }
    }
}
