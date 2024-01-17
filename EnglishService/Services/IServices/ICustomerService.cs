using EnglishService.ViewModels;

namespace EnglishService.Services.IServices
{
    public interface ICustomerService
    {
        Task<int?> GetCustomerIdByUserId(string userId);
        Task UpdateAsync(CustomerUpdateModel model);
        Task CreateAsync(CustomerCreateModel model, string userId);
        CustomerUpdateModel GetCustomerById(int customerId);
    }
}
