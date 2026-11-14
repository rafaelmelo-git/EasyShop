using System.Net.Mail;
using EasyShop.Data;
using EasyShop.Models;

namespace EasyShop.Services;

// Camada de negócio: validações e orquestração do cadastro de clientes.
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository) => _repository = repository;

    public Task<List<Customer>> GetAllAsync() => _repository.GetAllAsync();

    public async Task AddAsync(string name, string email)
    {
        name = (name ?? string.Empty).Trim();
        email = (email ?? string.Empty).Trim().ToLowerInvariant();

        if (name.Length < 3 || !name.Contains(' '))
            throw new BusinessException("Informe o nome completo (nome e sobrenome).");

        if (!IsValidEmail(email))
            throw new BusinessException("E-mail inválido.");

        if (await _repository.ExistsByEmailAsync(email))
            throw new BusinessException("Já existe um cliente com este e-mail.");

        await _repository.AddAsync(new Customer { Name = name, Email = email });
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('.')) return false;
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
