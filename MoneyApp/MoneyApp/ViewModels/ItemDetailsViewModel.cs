using Xamarin.Forms;
using MoneyApp.Models;
using SkiaSharp;
using Microcharts;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;
using System.IO;
using MoneyApp.View;

namespace MoneyApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class ItemDetailsViewModel : BaseViewModel
    {
        #region RouteParams
        //Параметр маршрута
        private string itemId;
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItem(value);
            }
        }
        #endregion

        #region Properties
        //Свойства
        public string ID { get; set; }
        public Command FavCommand { get; set; }
        public Command GoToCommand { get; set; }
        private bool IsFavItem { get; set; }
        #endregion

        #region INotify Properties
        //Name
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        //CharCode
        private string char_code;
        public string CharCode
        {
            get => char_code;
            set => SetProperty(ref char_code, value);
        }

        //Nominal
        private int nominal;
        public int Nominal
        {
            get => nominal;
            set => SetProperty(ref nominal, value);
        }

        //Value
        private decimal val;
        public decimal Value
        {
            get => val;
            set => SetProperty(ref val, value);
        }

        //Image
        private string image;
        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        //Chart
        private Chart chart;
        public Chart Chart1 
        {
            get => chart;
            set => SetProperty(ref chart, value);
        }

        //Иконка FavItem
        private string fav_img;
        public string FavImg
        {
            get => fav_img;
            set => SetProperty(ref fav_img, value);
        }

        //
        private float max_value;
        public float MaxValue
        {
            get => max_value;
            set => SetProperty(ref max_value, value);
        }

        private float min_value;
        public float MinValue
        {
            get => min_value;
            set => SetProperty(ref min_value, value);
        }

        private float value_change;
        public float ValueChange
        {
            get => value_change;
            set => SetProperty(ref value_change, value);
        }

        private string percent_value_change;
        public string PercentValueChange
        {
            get => percent_value_change;
            set => SetProperty(ref percent_value_change, value);
        }

        private float first;
        public float First 
        {
            get => first;
            set => SetProperty(ref first, value);
        }

        private float last;
        public float Last
        {
            get => last;
            set => SetProperty(ref last, value);
        }

        private string daily_change;
        public string DailyChange
        {
            get => daily_change;
            set => SetProperty(ref daily_change, value);
        }

        private string d_color;
        public string DailyColor
        {
            get => d_color;
            set => SetProperty(ref d_color, value);
        }
        #endregion

        #region Ctor
        public ItemDetailsViewModel()
        {
            DailyChange = "0.000 %";
            MaxValue = 0;
            MinValue = Single.MaxValue;
            ValueChange = 0;
            PercentValueChange = "0.000 %";

            FavCommand = new Command(ChangeFavItem);
            GoToCommand = new Command(GoToAnalyze);
        }
        #endregion

        #region Methods
        //Изменение FavItem
        public void ChangeFavItem()
        {
            if (IsFavItem)
            {
                DataStore.RemoveFavItem(ID);
                FavImg = "OffStar";
            }
            else if (!IsFavItem)
            {
                DataStore.AddFavItem(ID);
                FavImg = "OnStar";
            }
            IsFavItem = !IsFavItem;
        }

        //Первоначальная настройка FavImg
        private void SetFavItem()
        {
            if (IsFavItem)
            {
                FavImg = "OnStar";
            }
            else if (!IsFavItem)
            { 
                FavImg = "OffStar";
            }
        }

        //Загрузка элемента
        private void LoadItem(string id)
        {
            Quote item = DataStore.GetQuote(id);
            ID = item.Id;
            Name = item.Name;
            CharCode = item.CharCode;
            Nominal = item.Nominal;
            Value = item.Value;
            Image = item.Image;
            IsFavItem = item.IsFavorite;

            SetFavItem();
            LoadDaily();
        }

        //
        private void LoadDaily()
        {
            DateTime date = DateTime.Now;
            string date_2 = date.ToString("dd.MM.yyyy");
            date_2 = date_2.Replace('.', '/');
            date = date.AddDays(-4);
            string date_1 = date.ToString("dd.MM.yyyy");
            date_1 = date_1.Replace('.', '/');

            XmlDocument document = new XmlDocument();
            document.Load($"http://www.cbr.ru/scripts/XML_dynamic.asp?date_req1={date_1}&date_req2={date_2}&VAL_NM_RQ={ID}");
            XmlNode root = document.DocumentElement;
            XmlNodeList nodes = root.ChildNodes;

            if(nodes.Count >= 2)
            {
                float f = Convert.ToSingle(nodes[0].SelectSingleNode("./Value").InnerText);
                float l = Convert.ToSingle(nodes[nodes.Count - 1].SelectSingleNode("./Value").InnerText);

                float tmp = (l - f) / (f / 100);

                if (tmp < 0)
                {
                    DailyColor = "#f00";
                    DailyChange = "-" +(tmp / (-1)).ToString() + " %";
                }
                else
                {
                    DailyColor = "#11B502";
                    DailyChange = "+" + tmp.ToString() + " %";
                }
            }
        }

        //Обновление графика
        public Chart UpdateChart(string date_1, string date_2)
        {
            MaxValue = 0;
            MinValue = Single.MaxValue;

            List<ChartEntry> entries = new List<ChartEntry>();
            XmlDocument document = new XmlDocument();
            document.Load($"http://www.cbr.ru/scripts/XML_dynamic.asp?date_req1={date_1}&date_req2={date_2}&VAL_NM_RQ={ID}");
            XmlNode root = document.DocumentElement;
            XmlNodeList nodes = root.ChildNodes;
            foreach (XmlElement node in nodes)
            {
                float val = Convert.ToSingle(node.SelectSingleNode("./Value").InnerText);

                if (val > MaxValue)
                    MaxValue = val;

                if (val < MinValue)
                    MinValue = val;

                entries.Add(new ChartEntry(val) { Color = SKColor.Parse("#11B502") });
            }

            First = Convert.ToSingle(nodes[0].SelectSingleNode("./Value").InnerText);
            Last = Convert.ToSingle(nodes[nodes.Count - 1].SelectSingleNode("./Value").InnerText);

            ValueChange = Last - First;
            PercentValueChange = ((Last - First) / (First / 100)).ToString() + " %";

            return new LineChart() { Entries = entries, PointSize = 1, LineMode = LineMode.Straight, AnimationDuration = new TimeSpan(0, 0, 0, 0, 500), MaxValue = MaxValue + 1, MinValue = MinValue - 1 };
        }

        //
        public async void GoToAnalyze()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemAnalyzePage)}?{nameof(ItemAnalyzeViewModel.ItemId)}={ID}", true);
        }
        #endregion
    }
}
