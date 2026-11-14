using SQLite;

namespace EasyShop.Models;

// Entidade relacional (SQLite)
public class Customer
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [NotNull, Unique, MaxLength(150)]
    public string Email { get; set; } = string.Empty;
}
