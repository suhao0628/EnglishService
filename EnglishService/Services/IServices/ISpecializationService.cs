using EnglishService.Models;
using EnglishService.ViewModels;

namespace EnglishService.Services.IServices
{
    public interface ISpecializationService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
        Task CreateAsync(SpecializationCreateVM model);
        Task<IEnumerable<Specialization>> GetAllAsync();
        Task<bool> EditAsync(int specializationId, string name, string description);
        Task<bool> DeleteAsync(int specializationId);
        Task<Specialization> GetSpecializationByIdAsync(int specializationId);
    }
}
