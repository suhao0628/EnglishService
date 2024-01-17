using EnglishService.Models;
using EnglishService.Utities;
using Microsoft.AspNetCore.Identity;

namespace EnglishService.Data.SeedData
{
    public class AccountSeeder:ISeeder
    {
        public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            
            // Create Admin
            await CreateUser(
                userManager,
                roleManager, GlobalConstants.AdminEmail,
                GlobalConstants.AdministratorRoleName);

            // Create Professional
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.ProfessionalEmail,
                GlobalConstants.ProfessionalRoleName);

            // Create Customer
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.CustomerEmail,
                GlobalConstants.CustomerRoleName);

            // Create User
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.UserEmail);
        }

        private static async Task CreateUser(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, string email, string roleName = null)
        {
            var user = new AppUser
            {
                UserName = email,
                Email = email,
            };

            const string password = GlobalConstants.Password;

            if (roleName != null)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                if (!userManager.Users.Any(u => u.Roles.Any(r => r.RoleId == role.Id)))
                {
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }
            else
            {
                await userManager.CreateAsync(user, password);
            }
        }
    }
}
