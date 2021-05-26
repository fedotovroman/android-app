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
    public partial class SendEmailPage : ContentPage
    {
        public SendEmailPage()
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
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                emailMessenger.SendEmail("roman89203323606@gmail.com", ThemeTextBox.Text, BodyTextBox.Text);
            }
        }
    }
}