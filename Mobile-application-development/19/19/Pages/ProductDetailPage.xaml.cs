using _19.Models;
using _19.Services;

namespace _19.Pages;

[QueryProperty(nameof(ProductIdStr), "productId")]
public partial class ProductDetailPage : ContentPage
{
    private readonly StoreService _storeService = new();

    public string ProductIdStr { get; set; } = string.Empty;

    public ProductDetailPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadProduct();
    }

    private void LoadProduct()
    {
        Loader.IsRunning = false;
        Loader.IsVisible = false;

        if (string.IsNullOrWhiteSpace(ProductIdStr) || !int.TryParse(ProductIdStr.Trim(), out var productId))
        {
            EmptyState.IsVisible = true;
            ContentStack.IsVisible = false;
            return;
        }

        var product = _storeService.GetProductById(productId);
        if (product == null)
        {
            EmptyState.IsVisible = true;
            ContentStack.IsVisible = false;
            return;
        }

        EmptyState.IsVisible = false;
        ContentStack.IsVisible = true;
        ProductImage.Text = product.ImageUrl;
        ProductName.Text = product.Name;
        ProductDescription.Text = product.Description;
        ProductPrice.Text = product.FormattedPrice;
        ProductCategory.Text = $"Категория: {product.Category}";
        StockLabel.Text = product.InStock ? "✅ В наличии" : "❌ Нет в наличии";
    }
}
