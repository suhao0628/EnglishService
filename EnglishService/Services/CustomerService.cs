using EnglishService.Data;
using EnglishService.Models;
using EnglishService.Services.IServices;
using EnglishService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnglishService.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly AppDbContext _appDbContext;
        public CustomerService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int?> GetCustomerIdByUserId(string userId)
        {
            return await _appDbContext.Customers.Where(p => p.UserId == userId).Select(p => p.Id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(CustomerUpdateModel model)
        {
            var customer = _appDbContext.Customers.FirstOrDefault(d => d.Id == model.Id);

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Gender = model.Gender;
            customer.Age = model.Age;
            customer.RegionId = model.RegionId;
            customer.PhoneNumber = model.PhoneNumber;


            _appDbContext.Update(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(CustomerCreateModel model, string userId)
        {
            var customerUser = _appDbContext.AppUsers.FirstOrDefault(u => u.Id == userId);

            var customer = new Customer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Age = model.Age,
                PhoneNumber = model.PhoneNumber,
                RegionId = model.RegionId,
                UserId = model.UserId,
            };

            customerUser.Customer = customer;

            await _appDbContext.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public CustomerUpdateModel GetCustomerById(int customerId)
        {
            var customer=_appDbContext.Customers.Where(p => p.Id == customerId).FirstOrDefault();
            var customerUpdateModel = new CustomerUpdateModel()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName= customer.LastName,
                Gender = customer.Gender,
                Age = customer.Age,
                PhoneNumber = customer.PhoneNumber,
                RegionId = customer.RegionId,
               
            };
            return customerUpdateModel;
        }
    }
}
