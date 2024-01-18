using EnglishService.Data;
using EnglishService.Models;
using EnglishService.Services.IServices;
using EnglishService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnglishService.Services
{
    public class SpecializationService: ISpecializationService
    {
        private readonly AppDbContext _appDbContext;
        public SpecializationService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return _appDbContext.Specializations
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                })
               .ToList()
               .Select(s => new KeyValuePair<string, string>(s.Id.ToString(), s.Name));
        }

        public async Task CreateAsync(SpecializationCreateVM model)
        {
            var specialization = new Specialization
            {
                Name = model.Name,
                Description = model.Description
            };

            await _appDbContext.AddAsync(specialization);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync() => await _appDbContext.Specializations.ToListAsync();

        public async Task<bool> EditAsync(int specializationId, string name, string description)
        {
            var specialization = await GetSpecializationByIdAsync(specializationId);

            if (specialization == null)
            {
                return false;
            }

            specialization.Name = name;
            specialization.Description = description;

            _appDbContext.Update(specialization);

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int specializationId)
        {
            var specialization = await GetSpecializationByIdAsync(specializationId);

            if (specialization == null)
            {
                return false;
            }

            _appDbContext.Remove(specialization);

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Specialization> GetSpecializationByIdAsync(int specializationId) =>await  _appDbContext.Specializations.FirstOrDefaultAsync(s => s.Id == specializationId);
    }
}
