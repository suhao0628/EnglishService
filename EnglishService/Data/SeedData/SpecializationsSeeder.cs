using EnglishService.Models;

namespace EnglishService.Data.SeedData
{
    public class SpecializationsSeeder:ISeeder
    {
        public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Specializations.Any())
            {
                return;
            }

            var specializations = new[]
            {
                new Specialization{
                    Name = "Doctor",
                    Description = "Doctors are professionals in the field of medicine. They diagnose and treat various medical conditions, focusing on maintaining and restoring patients' health."
                },
                new Specialization
                {
                    Name = "Psychologist",
                    Description =
                    "Psychologists are experts in mental health. They diagnose and treat mental health issues, helping individuals cope with emotional and psychological challenges."
                },
new Specialization
{
    Name = "Trainer",
    Description =
        "Trainers specialize in physical fitness and exercise. They provide guidance on workouts, nutrition, and overall wellness, helping individuals achieve their fitness goals."
},
new Specialization
{
    Name = "Legal Advisors",
    Description =
        "Legal Advisors are professionals who offer legal consultation and advice. They assist individuals in understanding and navigating legal matters, ensuring compliance with laws and regulations."
},
new Specialization
{
    Name = "Beautician",
    Description =
        "Beauticians are experts in skincare, beauty, and cosmetic treatments. They provide services such as facials, skincare routines, and beauty enhancements to enhance individuals' appearance."
}
        };

            // Need them in particular order
            foreach (var specialization in specializations)
            {
                await dbContext.AddAsync(specialization);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
