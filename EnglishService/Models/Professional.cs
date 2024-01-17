using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnglishService.Models
{
    public class Professional
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public int Age { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; } 

        public int? Experience { get; set; } 

        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(500)]
        public string Resume { get; set; }


        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        [ForeignKey(nameof(Specialization))]
        public int SpecializationId { get; set; }

        public virtual Specialization Specialization { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual AppUser User { get; set; }

        public bool HasApplied { get; set; }

        public bool IsValidated { get; set; }
    }
}
