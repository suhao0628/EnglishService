using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace EnglishService.Models
{
    public class Image
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey(nameof(Professional))]
        public int ProfessionalId { get; set; }

        public virtual Professional Professional { get; set; }

        public string Extension { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
