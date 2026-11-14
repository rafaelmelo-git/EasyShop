using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyShop.Models;
using EasyShop.Services;

namespace EasyShop.ViewModels;

public partial class ProductsViewModel : ObservableObject
{
    private readonly IProductService _service;

    public ObservableCollection<Product> Products { get; } = new();

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string price = string.Empty;
    [ObservableProperty] private string attributes = string.Empty;
    [ObservableProperty] private string message = string.Empty;

    public ProductsViewModel(IProductService service) => _service = service;

    [RelayCommand]
    private async Task LoadAsync()
    {
        var items = await _service.GetAllAsync();
        Products.Clear();
        foreach (var p in items) Products.Add(p);
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            await _service.AddAsync(Name, Price, Attributes);
            Name = Price = Attributes = string.Empty;
            Message = "Produto salvo com sucesso!";
            await LoadAsync();
        }
        catch (BusinessException ex)
        {
            Message = ex.Message;
        }
    }
}
