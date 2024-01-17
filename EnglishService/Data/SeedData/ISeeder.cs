namespace EnglishService.Data.SeedData
{
    public interface ISeeder
    {
        Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider);
    }
}
