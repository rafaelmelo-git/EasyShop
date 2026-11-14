using EasyShop.Models;
using LiteDB;

namespace EasyShop.Data;

// Camada de dados: NoSQL de documentos (LiteDB). Só faz CRUD, sem regra de negócio.
public class ProductRepository : IProductRepository, IDisposable
{
    private readonly LiteDatabase _db;
    private readonly ILiteCollection<Product> _collection;

    public ProductRepository()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "products.db");
        _db = new LiteDatabase(path);
        _collection = _db.GetCollection<Product>("products");
    }

    public Task<List<Product>> GetAllAsync() =>
        Task.Run(() => _collection.FindAll().OrderByDescending(p => p.Id).ToList());

    public Task AddAsync(Product product) =>
        Task.Run(() => { _collection.Insert(product); });

    public void Dispose() => _db.Dispose();
}
