using EasyShop.Models;

namespace EasyShop.Data;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);
}
