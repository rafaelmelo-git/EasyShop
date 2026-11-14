using EasyShop.Models;
using SQLite;

namespace EasyShop.Data;

// Camada de dados: SQL relacional (SQLite). Só faz CRUD, sem regra de negócio.
public class CustomerRepository : ICustomerRepository
{
    private readonly SQLiteAsyncConnection _db;
    private bool _initialized;

    public CustomerRepository()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "customers.db3");
        _db = new SQLiteAsyncConnection(path);
    }

    private async Task InitAsync()
    {
        if (_initialized) return;
        await _db.CreateTableAsync<Customer>();
        _initialized = true;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        await InitAsync();
        return await _db.Table<Customer>().OrderByDescending(c => c.Id).ToListAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        await InitAsync();
        var count = await _db.Table<Customer>()
            .Where(c => c.Email == email)
            .CountAsync();
        return count > 0;
    }

    public async Task AddAsync(Customer customer)
    {
        await InitAsync();
        await _db.InsertAsync(customer);
    }
}
