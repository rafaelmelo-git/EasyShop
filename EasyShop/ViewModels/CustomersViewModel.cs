using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyShop.Models;
using EasyShop.Services;

namespace EasyShop.ViewModels;

public partial class CustomersViewModel : ObservableObject
{
    private readonly ICustomerService _service;

    public ObservableCollection<Customer> Customers { get; } = new();

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string message = string.Empty;

    public CustomersViewModel(ICustomerService service) => _service = service;

    [RelayCommand]
    private async Task LoadAsync()
    {
        var items = await _service.GetAllAsync();
        Customers.Clear();
        foreach (var c in items) Customers.Add(c);
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            await _service.AddAsync(Name, Email);
            Name = Email = string.Empty;
            Message = "Cliente salvo com sucesso!";
            await LoadAsync();
        }
        catch (BusinessException ex)
        {
            Message = ex.Message;
        }
    }
}
