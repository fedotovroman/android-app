using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MoneyApp.ViewModels;
using System;
using MoneyApp.Sensors;

namespace MoneyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailsPage : ContentPage
    {
        ItemDetailsViewModel _context;

        public ItemDetailsPage()
        {
            InitializeComponent();

            BindingContext = _context = new ItemDetailsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AccelerometerSensor.LastPage = this.GetType().Name;

            DisableButtons();
            EnableButton(3);

            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddMonths(-1);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            MainChart.Chart = _context.UpdateChart(date_1, date_2);
        }


        private void TenYears_Clicked(object sender, EventArgs e)
        {
            DisableButtons();
            EnableButton(1);

            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddYears(-2);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            MainChart.Chart = _context.UpdateChart(date_1, date_2);
        }

        private void Year_Clicked(object sender, System.EventArgs e)
        {
            DisableButtons();
            EnableButton(2);

            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddYears(-1);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            MainChart.Chart = _context.UpdateChart(date_1, date_2);
        }

        private void Month_Clicked(object sender, EventArgs e)
        {
            DisableButtons();
            EnableButton(3);

            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddMonths(-1);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            MainChart.Chart = _context.UpdateChart(date_1, date_2);
        }

        private void Week_Clicked(object sender, EventArgs e)
        {
            DisableButtons();
            EnableButton(4);

            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddDays(-7);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            MainChart.Chart = _context.UpdateChart(date_1, date_2);
        }

        //
        private void DisableButtons()
        {
            Btn_1.BackgroundColor = Color.FromHex("#cfcfcf");
            Btn_2.BackgroundColor = Color.FromHex("#cfcfcf");
            Btn_3.BackgroundColor = Color.FromHex("#cfcfcf");
            Btn_4.BackgroundColor = Color.FromHex("#cfcfcf");
        }

        private void EnableButton(int id)
        {
            switch (id)
            {
                case 1:
                    Btn_1.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 2:
                    Btn_2.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 3:
                    Btn_3.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 4:
                    Btn_4.BackgroundColor = Color.FromHex("#85ccff");
                    break;
            }
        }
    }
}