using EnglishService.Models;
using EnglishService.Utities;
using Microsoft.EntityFrameworkCore;

namespace EnglishService.Data.SeedData
{
    public class CustomerSeeder:ISeeder
    {
        public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Customers.Any())
            {
                return;
            }

            var patientUser = await dbContext.AppUsers
                .Where(u => u.Email == GlobalConstants.CustomerEmail).FirstOrDefaultAsync();

            var patient = new Customer()
            {
                FirstName = "su",
                LastName = "customer",
                Age = 24,
                Gender = "Male",
                PhoneNumber = "0885254553",
                RegionId = 2,
                User = patientUser,
            };

            patientUser.Customer = patient;

            await dbContext.Customers.AddAsync(patient);
            await dbContext.SaveChangesAsync();
        }
    }
}
