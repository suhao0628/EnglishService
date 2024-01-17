using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace EnglishService.Models
{
    public class AppUser : IdentityUser
    {
        [ForeignKey(nameof(Customer))]
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Professional))]
        public int? ProfessionalId { get; set; }

        public virtual Professional Professional { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; } = new List<IdentityUserRole<string>>();

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new List<IdentityUserClaim<string>>();

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; } = new List<IdentityUserLogin<string>>();
    }
}
