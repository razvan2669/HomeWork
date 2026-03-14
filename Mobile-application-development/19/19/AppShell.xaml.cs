using _19.Pages;

namespace _19;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("ProductDetail", typeof(ProductDetailPage));
    }
}
