using EasyShop.Models;

namespace EasyShop.Services;

public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync();
    Task AddAsync(string name, string email);
}
