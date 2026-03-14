using System.Collections.ObjectModel;
using _19.Models;
using _19.Services;

namespace _19.Pages;

public partial class StorePage : ContentPage
{
    private readonly StoreService _storeService = new();
    private List<ProductDisplayModel> _allProducts = [];

    public StorePage()
    {
        InitializeComponent();
        LoadProducts();
    }

    private void LoadProducts()
    {
        _allProducts = _storeService.GetAllProducts();
        ApplyFilter(null);
    }

    private void OnSearchChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter(SearchEntry.Text);
    }

    private void ApplyFilter(string? search)
    {
        var filtered = string.IsNullOrWhiteSpace(search)
            ? _allProducts
            : _allProducts.Where(p =>
                p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        ProductsList.ItemsSource = new ObservableCollection<ProductDisplayModel>(filtered);
    }

    private async void OnProductSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ProductDisplayModel product)
        {
            ProductsList.SelectedItem = null;
            await NavigateToProduct(product.Id);
        }
    }

    private async void OnDetailsClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is ProductDisplayModel product)
        {
            await NavigateToProduct(product.Id);
        }
    }

    private async Task NavigateToProduct(int productId)
    {
        await Shell.Current.GoToAsync($"ProductDetail?productId={productId}");
    }
}
