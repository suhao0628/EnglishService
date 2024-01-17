using EnglishService.Data;
using EnglishService.Models;
using EnglishService.Services.IServices;
using EnglishService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnglishService.Services
{
    public class RegionService: IRegionService
    {
        private readonly AppDbContext _appDbContext;
        public RegionService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return _appDbContext.Regions
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                })
                .ToList()
                .Select(s => new KeyValuePair<string, string>(s.Id.ToString(), s.Name));
        }

        public async Task CreateAsync(RegionCreateModel model)
        {
            var specialization = new Region
            {
                Name = model.Name,
                PostCode = model.PostCode
            };

            await _appDbContext.AddAsync(specialization);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int regionId, string name, int? postCode)
        {
            var region = await GetRegionByIdAsync(regionId);

            if (region == null)
            {
                return false;
            }

            region.Name = name;
            region.PostCode = postCode;

            _appDbContext.Update(region);

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int regionId)
        {
            var town = await GetRegionByIdAsync(regionId);

            if (town == null)
            {
                return false;
            }

            _appDbContext.Remove(town);

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public Task<Region> GetRegionByIdAsync(int regionId)
        {
            return _appDbContext.Regions.FirstOrDefaultAsync(s => s.Id == regionId);
        }
    }
}
