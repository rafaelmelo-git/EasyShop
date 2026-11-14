using EasyShop.Models;

namespace EasyShop.Services;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task AddAsync(string name, string priceText, string attributesText);
}
