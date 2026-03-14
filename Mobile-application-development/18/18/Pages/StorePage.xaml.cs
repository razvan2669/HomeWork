using _18.ViewModels;

namespace _18.Pages;

public partial class StorePage : ContentPage
{
    public StorePage(StoreViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnProductSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.ProductDisplayModel product)
        {
            ProductsList.SelectedItem = null;
            await Shell.Current.GoToAsync($"ProductDetail?productId={product.Id}");
        }
    }
}
