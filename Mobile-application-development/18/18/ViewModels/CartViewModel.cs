using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using _18.Models;
using _18.Services;

namespace _18.ViewModels;

public partial class CartViewModel : ObservableObject
{
    private readonly StoreService _storeService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsCartEmpty))]
    [NotifyPropertyChangedFor(nameof(IsCheckoutVisible))]
    private ObservableCollection<CartItemDisplay> _items = [];

    [ObservableProperty]
    private decimal _totalAmount;

    [ObservableProperty]
    private string _customerName = string.Empty;

    [ObservableProperty]
    private string _customerEmail = string.Empty;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public bool IsCartEmpty => Items.Count == 0;

    public bool IsCheckoutVisible => !IsCartEmpty;

    public CartViewModel(StoreService storeService)
    {
        _storeService = storeService;
        _ = LoadCartAsync();
    }

    /// <summary>Вызвать при появлении страницы для обновления корзины</summary>
    public async Task LoadAsync() => await LoadCartAsync();

    [RelayCommand]
    private async Task LoadCartAsync()
    {
        var cartItems = await _storeService.GetCartItemsAsync();
        Items = new ObservableCollection<CartItemDisplay>(cartItems);
        TotalAmount = Items.Sum(x => x.Subtotal);
        OnPropertyChanged(nameof(IsCartEmpty));
    }

    [RelayCommand]
    private async Task RemoveFromCartAsync(CartItemDisplay? item)
    {
        if (item == null) return;
        await _storeService.RemoveFromCartAsync(item.ProductId);
        await LoadCartAsync();
    }

    [RelayCommand]
    private async Task CreateOrderAsync()
    {
        if (Items.Count == 0)
        {
            StatusMessage = "Корзина пуста";
            return;
        }
        if (string.IsNullOrWhiteSpace(CustomerName) || string.IsNullOrWhiteSpace(CustomerEmail))
        {
            StatusMessage = "Введите имя и email";
            return;
        }

        var customer = await _storeService.GetOrCreateCustomerAsync(CustomerName.Trim(), CustomerEmail.Trim());
        var order = await _storeService.CreateOrderFromCartAsync(customer.CustomerId);
        if (order != null)
        {
            StatusMessage = $"Заказ #{order.OrderId} оформлен на сумму {order.TotalAmount:N0} ₽";
            await LoadCartAsync();
            CustomerName = string.Empty;
            CustomerEmail = string.Empty;
        }
    }
}
