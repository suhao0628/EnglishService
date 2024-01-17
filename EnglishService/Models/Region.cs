using System.ComponentModel.DataAnnotations;

namespace EnglishService.Models
{
    public class Region
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int? PostCode { get; set; }
    }
}
