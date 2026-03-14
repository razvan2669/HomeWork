using _18.Pages;

namespace _18
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ProductDetail", typeof(ProductDetailPage));
        }
    }
}
