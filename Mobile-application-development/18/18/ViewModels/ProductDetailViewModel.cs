using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using _18.Models;
using _18.Services;

namespace _18.ViewModels;

[QueryProperty(nameof(ProductIdStr), "productId")]
public partial class ProductDetailViewModel : ObservableObject
{
    private readonly StoreService _storeService;

    [ObservableProperty]
    private string _productIdStr = string.Empty;

    private int _productId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsProductLoaded))]
    [NotifyPropertyChangedFor(nameof(IsLoading))]
    [NotifyPropertyChangedFor(nameof(StockStatusText))]
    private ProductDisplayModel? _product;

    [ObservableProperty]
    private int _quantity = 1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasStatusMessage))]
    private string _statusMessage = string.Empty;

    /// <summary>Товар загружен (для видимости UI)</summary>
    public bool IsProductLoaded => Product != null;

    /// <summary>Текст наличия на складе</summary>
    public string StockStatusText => Product?.InStock == true ? "В наличии" : "Нет в наличии";

    /// <summary>Есть ли сообщение о статусе</summary>
    public bool HasStatusMessage => !string.IsNullOrEmpty(StatusMessage);

    /// <summary>Идёт загрузка товара</summary>
    public bool IsLoading => Product == null && _productId > 0;

    public ProductDetailViewModel(StoreService storeService)
    {
        _storeService = storeService;
    }

    partial void OnProductIdStrChanged(string value)
    {
        Product = null;
        if (string.IsNullOrWhiteSpace(value) || !int.TryParse(value.Trim(), out _productId))
        {
            _productId = 0;
            OnPropertyChanged(nameof(IsLoading));
            OnPropertyChanged(nameof(IsProductLoaded));
            return;
        }
        _ = LoadProductAsync();
    }

    [RelayCommand]
    private async Task LoadProductAsync()
    {
        if (_productId <= 0) return;
        Product = await _storeService.GetProductDisplayAsync(_productId);
    }

    [RelayCommand]
    private void DecreaseQuantity()
    {
        if (Quantity > 1) Quantity--;
    }

    [RelayCommand]
    private void IncreaseQuantity()
    {
        Quantity++;
    }

    [RelayCommand]
    private async Task AddToCartAsync()
    {
        if (Product == null || Quantity < 1) return;
        await _storeService.AddToCartAsync(Product.Id, Quantity);
        StatusMessage = $"Добавлено {Quantity} шт. в корзину";
    }
}
