using AutoMapper;
using EnglishService.Models;
using EnglishService.Utities;

namespace EnglishService.ViewModels
{
    public class AppointmentsCustomerVM
    {
        public int Id { get; set; }

        public string DateTime { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerRegionName { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public bool? IsConfirmed { get; set; }

        public int? Rating { get; set; }

        public string RatingComment { get; set; }
    }
}
