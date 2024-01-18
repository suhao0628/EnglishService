using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace EnglishService.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [Required]
        public int Number { get; set; } // 0-10

        public DateTime CreatedOn { get; set; }

        public string Comment { get; set; }

        [ForeignKey(nameof(Professional))]
        public int ProfessionalId { get; set; }

        public virtual Professional Professional { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
