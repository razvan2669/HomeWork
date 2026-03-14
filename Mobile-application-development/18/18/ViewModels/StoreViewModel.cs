using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using _18.Models;
using _18.Services;

namespace _18.ViewModels;

public partial class StoreViewModel : ObservableObject
{
    private readonly StoreService _storeService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FilteredProducts))]
    private ObservableCollection<ProductDisplayModel> _products = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FilteredProducts))]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private ProductDisplayModel? _selectedProduct;

    [ObservableProperty]
    private bool _isRefreshing;

    public IEnumerable<ProductDisplayModel> FilteredProducts =>
        string.IsNullOrWhiteSpace(SearchText)
            ? Products
            : Products.Where(p =>
                p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

    public StoreViewModel(StoreService storeService)
    {
        _storeService = storeService;
        _ = LoadProductsAsync();
    }

    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        var products = await _storeService.GetAllProductsAsync();
        Products = new ObservableCollection<ProductDisplayModel>(products);
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        try
        {
            IsRefreshing = true;
            await LoadProductsAsync();
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task GoToProductAsync(ProductDisplayModel? product)
    {
        if (product == null || product.Id <= 0) return;
        await Shell.Current.GoToAsync($"ProductDetail?productId={product.Id}");
    }

    [RelayCommand]
    private async Task AddToCartAsync(ProductDisplayModel? product)
    {
        if (product == null || product.Id <= 0) return;
        await _storeService.AddToCartAsync(product.Id, 1);
    }

    partial void OnSearchTextChanged(string value) => OnPropertyChanged(nameof(FilteredProducts));
    partial void OnProductsChanged(ObservableCollection<ProductDisplayModel> value) => OnPropertyChanged(nameof(FilteredProducts));
}
