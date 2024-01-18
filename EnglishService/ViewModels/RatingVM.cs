using AutoMapper;
using EnglishService.Models;
using EnglishService.Utities;

namespace EnglishService.ViewModels
{
    public class RatingVM
    {
        public string CustomerFullName { get; set; }

        public int Number { get; set; }

        public string Comment { get; set; }

        public string CreatedOn { get; set; }
    }
}
