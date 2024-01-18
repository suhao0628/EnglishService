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
                    Name = "Kirovsky",
                    PostCode = 660000
                },
                new Region
                {
                    Name = "Leninsky",
                    PostCode = 664000
                },
                new Region
                {
                    Name = "Oktyabrsky",
                    PostCode = 660004
                },
                new Region
                {
                    Name = "Sovetsky",
                    PostCode = 660008
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
