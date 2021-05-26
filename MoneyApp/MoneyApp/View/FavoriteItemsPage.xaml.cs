using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MoneyApp.Sensors;
using MoneyApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteItemsPage : ContentPage
    {
        FavoriteItemsViewModel _context;

        public FavoriteItemsPage()
        {
            InitializeComponent();

            BindingContext = _context = new FavoriteItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AccelerometerSensor.LastPage = this.GetType().Name;
            _context.OnAppearing();
        }
    }
}
