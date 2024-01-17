using EnglishService.Models;
using EnglishService.ViewModels;

namespace EnglishService.Services.IServices
{
    public interface IRegionService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
        Task CreateAsync(RegionCreateModel model);
        Task<bool> UpdateAsync(int regionId, string name, int? postCode);
        Task<bool> DeleteAsync(int regionId);
        Task<Region> GetRegionByIdAsync(int regionId);
    }
}
