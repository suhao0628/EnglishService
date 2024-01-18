using EnglishService.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EnglishService.ViewModels
{
    public class MakeAppointmentVM
    {
        [Required]
        [ValidateDate]
        public string Date { get; set; }

        [Required]
        [ValidateTime]
        public string Time { get; set; }

        [Required]
        public int ProfessionalId { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}
