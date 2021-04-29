using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MoneyApp.ViewModels;

namespace MoneyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _context;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _context = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _context.OnAppearing();
        }
    }
}