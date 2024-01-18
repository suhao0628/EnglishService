using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace EnglishService.Models
{
    public class Appointment
    {
        [Key] 
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey(nameof(Professional))]
        public int ProfessionalId { get; set; }

        public virtual Professional Professional { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public bool? IsConfirmed { get; set; }

        public bool IsRated { get; set; }

        [ForeignKey(nameof(Rating))]
        [ValidateNever]
        public int? RatingId { get; set; }

        public virtual Rating Rating { get; set; }
    }
}
