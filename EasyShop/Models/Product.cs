using LiteDB;

namespace EasyShop.Models;

// Documento NoSQL (LiteDB). Attributes guarda atributos dinâmicos sem schema fixo.
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Dictionary<string, string> Attributes { get; set; } = new();

    [BsonIgnore]
    public string AttributesSummary =>
        Attributes.Count == 0
            ? "Sem atributos"
            : string.Join(" | ", Attributes.Select(a => $"{a.Key}: {a.Value}"));

    [BsonIgnore]
    public string PriceDisplay => Price.ToString("C");
}
