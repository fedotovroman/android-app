using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MoneyApp.View;

namespace MoneyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemDetailsPage), typeof(ItemDetailsPage));
            Routing.RegisterRoute(nameof(SendEmailPage), typeof(SendEmailPage));
            Routing.RegisterRoute(nameof(ItemAnalyzePage), typeof(ItemAnalyzePage));
        }
    }
}