using EnglishService.Models;

namespace EnglishService.Data.SeedData
{
    public class RegionSeeder:ISeeder
    {
        public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Regions.Any())
            {
                return;
            }

            var regions = new Region[]
            {
                new Region
                {
                    Name = "Sofia",
                    PostCode = 1000
                },
                new Region
                {
                    Name = "Plovdiv",
                    PostCode = 4000
                },
                new Region
                {
                    Name = "Varna",
                    PostCode = 9000
                },
                new Region
                {
                    Name = "Burgas",
                    PostCode = 8000
                },
            };

            foreach (var region in regions)
            {
                await dbContext.AddAsync(region);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
