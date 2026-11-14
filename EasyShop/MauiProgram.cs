using EasyShop.Data;
using EasyShop.Services;
using EasyShop.ViewModels;
using EasyShop.Views;

namespace EasyShop;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        // Camada de dados
        builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();

        // Camada de negócio
        builder.Services.AddSingleton<ICustomerService, CustomerService>();
        builder.Services.AddSingleton<IProductService, ProductService>();

        // Camada de apresentação
        builder.Services.AddTransient<CustomersViewModel>();
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<CustomersPage>();
        builder.Services.AddTransient<ProductsPage>();

        return builder.Build();
    }
}
