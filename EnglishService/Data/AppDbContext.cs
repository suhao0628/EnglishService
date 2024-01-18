using EnglishService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnglishService.Data;

//public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
//{
//}
//public DbSet<AppUser> AppUsers { get; set; }
public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Professional> Professionals { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Image> Images { get; set; }

    public DbSet<Rating> Ratings { get; set; }


}

