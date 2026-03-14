using _18.ViewModels;

namespace _18.Pages;

public partial class CartPage : ContentPage
{
    public CartPage(CartViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CartViewModel viewModel)
            await viewModel.LoadAsync();
    }
}
