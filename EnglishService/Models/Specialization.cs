using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace EnglishService.Models
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Professional> Professionals { get; set; } = new List<Professional>();
    }
}
