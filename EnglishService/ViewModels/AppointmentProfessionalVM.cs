using AutoMapper;
using EnglishService.Models;

namespace EnglishService.ViewModels
{
    public class AppointmentProfessionalVM
    {
        public int Id { get; set; }

        public string DateTime { get; set; }

        public int ProfessionalId { get; set; }

        public string ProfessionalFullName { get; set; }

        public string ProfessionalRegionName { get; set; }

        public string ProfessionalAddress { get; set; }

        public bool? IsConfirmed { get; set; }

        public bool IsRated { get; set; }
    }
}
