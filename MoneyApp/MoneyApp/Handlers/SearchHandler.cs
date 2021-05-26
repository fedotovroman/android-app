using MoneyApp.ViewModels;
using Xamarin.Forms;

namespace MoneyApp.Handlers
{
    class SearchHandler : Xamarin.Forms.SearchHandler
    {
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            ((ItemsViewModel)this.BindingContext).SearchString = newValue;
            ((ItemsViewModel)this.BindingContext).LoadItems();
        }
    }
}
