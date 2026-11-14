using System.Globalization;
using EasyShop.Data;
using EasyShop.Models;

namespace EasyShop.Services;

// Camada de negócio: valida o produto e converte o texto de atributos em dicionário.
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository) => _repository = repository;

    public Task<List<Product>> GetAllAsync() => _repository.GetAllAsync();

    public async Task AddAsync(string name, string priceText, string attributesText)
    {
        name = (name ?? string.Empty).Trim();
        if (name.Length == 0)
            throw new BusinessException("Informe o nome do produto.");

        if (!TryParsePrice(priceText, out var price) || price < 0)
            throw new BusinessException("Informe um preço válido.");

        var product = new Product
        {
            Name = name,
            Price = price,
            Attributes = ParseAttributes(attributesText)
        };

        await _repository.AddAsync(product);
    }

    private static bool TryParsePrice(string? text, out decimal price)
    {
        text = (text ?? string.Empty).Trim();
        return decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out price)
            || decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out price);
    }

    // Formato: uma linha por atributo, "chave: valor" (ex.: "Cor: Azul")
    private static Dictionary<string, string> ParseAttributes(string? text)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (string.IsNullOrWhiteSpace(text)) return result;

        var lines = text.Split(new[] { '\r', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var idx = line.IndexOf(':');
            if (idx <= 0)
                throw new BusinessException($"Atributo inválido: \"{line.Trim()}\". Use o formato chave: valor.");

            var key = line[..idx].Trim();
            var value = line[(idx + 1)..].Trim();
            if (key.Length == 0 || value.Length == 0)
                throw new BusinessException($"Atributo inválido: \"{line.Trim()}\".");

            result[key] = value;
        }
        return result;
    }
}
