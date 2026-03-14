using _18.ViewModels;

namespace _18.Pages;

public partial class ProductDetailPage : ContentPage
{
    public ProductDetailPage(ProductDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
