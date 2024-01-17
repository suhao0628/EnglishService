using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnglishService.Models
{
    public class Customer
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

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual AppUser User { get; set; }

    }
}
