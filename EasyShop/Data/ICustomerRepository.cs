using EasyShop.Models;

namespace EasyShop.Data;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(Customer customer);
}
