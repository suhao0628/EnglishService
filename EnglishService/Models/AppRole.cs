using Microsoft.AspNetCore.Identity;

namespace EnglishService.Models
{
    public class AppRole: IdentityRole
    {
        public AppRole()
            : this(null)
        {
        }

        public AppRole(string name)
            : base(name)
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
