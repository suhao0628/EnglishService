using AutoMapper;
using EnglishService.Models;
using System.Numerics;

namespace EnglishService.ViewModels
{
    public class ProfessionalInfoListVM
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public double? AverageRating { get; set; }

        public int? Experience { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Resume { get; set; }

        public string RegionName { get; set; }

        public string SpecializationName { get; set; }

        public bool IsApplied { get; set; }

        public bool IsApproved { get; set; }

        
    }
}
