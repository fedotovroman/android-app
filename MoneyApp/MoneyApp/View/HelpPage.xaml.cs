using MoneyApp.Sensors;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AccelerometerSensor.LastPage = this.GetType().Name;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"{nameof(SendEmailPage)}");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall("+79203323606");
        }
    }
}