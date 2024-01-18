using EnglishService.Models;
using EnglishService.ViewModels;

namespace EnglishService.Services.IServices
{
    public interface IProfessionalService
    {
        Task UpdateAsync(ProfessionalEditVM model, string imagePath);
        Task CreateAsync(ProfessionalCreateVM model, string imagePath);
        //IEnumerable<T> GetAllValidatedProfessionals<T>(int page, int itemsPerPage);
        IEnumerable<ProfessionalInfoListVM> GetAllValidatedProfessionals(int page, int itemsPerPage);
        //IEnumerable<T> GetAllValidatedProfessionals<T>(int page, int itemsPerPage, string searchContent, int? regionId, int? specializationId);
        IEnumerable<ProfessionalInfoListVM> GetAllValidatedProfessionals(int page, int itemsPerPage, string searchContent, int? regionId, int? specializationId);
       // IEnumerable<T> GetAllAppliedProfessionals<T>(int page, int itemsPerPage);
        int GetAppliedAndNotValidatedProfessionalsCount();
        int? GetProfessionalIdByUserId(string userId);
        //T GetProfessionalById<T>(int professionalId);
        public Professional GetProfessionalById(int professionalId);
        
        Task<bool> VerifyAsync(int professionalId, string userId);
        Task<bool> DeleteAsync(int professionalId);
        ProfessionalInfoListVM GetProfessionalListById(int professionalId);
        ProfessionalEditVM GetProfessionalForUpdate(int professionalId);
        IEnumerable<ProfessionalInfoListVM> GetAllAppliedProfessionals(int page, int itemsPerPage);
    }
}
