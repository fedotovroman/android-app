using MoneyApp.Sensors;
using MoneyApp.ViewModels;
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
    public partial class ItemAnalyzePage : ContentPage
    {
        ItemAnalyzeViewModel _context;
        int curr_chart_id;

        public ItemAnalyzePage()
        {
            InitializeComponent();
            
            BindingContext = _context = new ItemAnalyzeViewModel();
            curr_chart_id = 1;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AccelerometerSensor.LastPage = this.GetType().Name;

            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddMonths(-1);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            _context.LoadData(date_1, date_2);

            DisableChartButtons();
            DisableDateButtons();

            EnableChartBtn(1);
            EnableDateBtn(3);
            //MainChart.Chart = _context.UpdateChart();
        }

        //
        private void MainBtn_Clicked(object sender, EventArgs e)
        {
            EnableChartBtn(1);
            MainChart.Chart = _context.UpdateChart();
        }

        private void SMABtn_Clicked(object sender, EventArgs e)
        {
            EnableChartBtn(2);
            MainChart.Chart = _context.UpdateSMAChart();
        }

        private void EMABtn_Clicked(object sender, EventArgs e)
        {
            EnableChartBtn(3);
            MainChart.Chart = _context.UpdateEMAChart();
        }

        private void BBLine1Btn_Clicked(object sender, EventArgs e)
        {
            EnableChartBtn(4);
            MainChart.Chart = _context.UpdateBBLine_1Chart();
        }

        private void BBLine2Btn_Clicked(object sender, EventArgs e)
        {
            EnableChartBtn(5);
            MainChart.Chart = _context.UpdateBBLine_2Chart();
        }

        //
        private void Button_Clicked(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddYears(-2);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            _context.LoadData(date_1, date_2);
            EnableDateBtn(1);
            //MainChart.Chart = _context.UpdateChart();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddYears(-1);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            _context.LoadData(date_1, date_2);
            EnableDateBtn(2);
            //MainChart.Chart = _context.UpdateChart();
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddMonths(-1);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            _context.LoadData(date_1, date_2);
            EnableDateBtn(3);
            //MainChart.Chart = _context.UpdateChart();
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddDays(-7);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            _context.LoadData(date_1, date_2);
            EnableDateBtn(4);
            //MainChart.Chart = _context.UpdateChart();
        }

        //
        private void DisableDateButtons()
        {
            TwoYearsBtn.BackgroundColor = Color.FromHex("#cfcfcf");
            OneYearBtn.BackgroundColor = Color.FromHex("#cfcfcf");
            OneMonthBtn.BackgroundColor = Color.FromHex("#cfcfcf");
            OneWeekBtn.BackgroundColor = Color.FromHex("#cfcfcf");
        }

        private void DisableChartButtons()
        {
            MainBtn.BackgroundColor = Color.FromHex("#cfcfcf");
            SMABtn.BackgroundColor = Color.FromHex("#cfcfcf");
            EMABtn.BackgroundColor = Color.FromHex("#cfcfcf");
            BBLine1Btn.BackgroundColor = Color.FromHex("#cfcfcf");
            BBLine2Btn.BackgroundColor = Color.FromHex("#cfcfcf");
        }

        private void EnableDateBtn(int id)
        {
            switch (id)
            {
                case 1:
                    DisableDateButtons();
                    TwoYearsBtn.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 2:
                    DisableDateButtons();
                    OneYearBtn.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 3:
                    DisableDateButtons();
                    OneMonthBtn.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 4:
                    DisableDateButtons();
                    OneWeekBtn.BackgroundColor = Color.FromHex("#85ccff");
                    break;
            }

            switch (curr_chart_id)
            {
                case 1:
                    MainChart.Chart = _context.UpdateChart();
                    break;
                case 2:
                    MainChart.Chart = _context.UpdateSMAChart();
                    break;
                case 3:
                    MainChart.Chart = _context.UpdateEMAChart();
                    break;
                case 4:
                    MainChart.Chart = _context.UpdateBBLine_1Chart();
                    break;
                case 5:
                    MainChart.Chart = _context.UpdateBBLine_2Chart();
                    break;
            }
        }

        private void EnableChartBtn(int id)
        {
            curr_chart_id = id;
            switch (id)
            {
                case 1:
                    DisableChartButtons();
                    MainBtn.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 2:
                    DisableChartButtons();
                    SMABtn.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 3:
                    DisableChartButtons();
                    EMABtn.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 4:
                    DisableChartButtons();
                    BBLine1Btn.BackgroundColor = Color.FromHex("#85ccff");
                    break;
                case 5:
                    DisableChartButtons();
                    BBLine2Btn.BackgroundColor = Color.FromHex("#85ccff");
                    break;
            }
        }
    }
}