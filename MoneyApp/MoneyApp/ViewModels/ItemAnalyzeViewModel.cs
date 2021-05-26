using Microcharts;
using MoneyApp.Data;
using MoneyApp.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Xamarin.Forms;

namespace MoneyApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class ItemAnalyzeViewModel : BaseViewModel
    {
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

        //Свойства
        public string ID { get; set; }

        //INotify св-ва
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

        //
        private float MaxValue { get; set; }
        private float MinValue { get; set; }
        private List<float> Data { get; set; }

        //Загрузка предемета
        private void LoadItem(string id)
        {
            Quote item = DataStore.GetQuote(id);
            ID = item.Id;
            Name = item.Name;
            CharCode = item.CharCode;
            Nominal = item.Nominal;
            Value = item.Value;
            Image = item.Image;
        }

        //Загрузка данных для графика
        public void LoadData(string date_1, string date_2)
        {
            MaxValue = 0;
            MinValue = Single.MaxValue;

            Data = new List<float>();
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

                Data.Add(val);
            }
        }

        //Обновление графика
        public Chart UpdateChart()
        {
            List<ChartEntry> entries = new List<ChartEntry>();

            foreach (float val in Data)
                entries.Add(new ChartEntry(val) { Color = SKColor.Parse("#11B502") });

            return new LineChart() { Entries = entries, PointSize = 1, LineMode = LineMode.Straight, AnimationDuration = new TimeSpan(0, 0, 0, 0, 500), MaxValue = MaxValue + 1, MinValue = MinValue - 1 };
        }

        public Chart UpdateEMAChart()
        {
            List<ChartEntry> entries = new List<ChartEntry>();

            ICollection<float> _data = (ICollection<float>)Data;

            DataAnalyzer analyser = new DataAnalyzer(_data);
            var ema_data = analyser.getEMA();

            foreach (float val in ema_data)
                entries.Add(new ChartEntry(val) { Color = SKColor.Parse("#f00") });

            return new LineChart() { Entries = entries, PointSize = 1, LineMode = LineMode.Straight, AnimationDuration = new TimeSpan(0, 0, 0, 0, 500), MaxValue = MaxValue + 1, MinValue = MinValue - 1 };
        }

        public Chart UpdateSMAChart()
        {
            List<ChartEntry> entries = new List<ChartEntry>();

            ICollection<float> _data = (ICollection<float>)Data;

            DataAnalyzer analyser = new DataAnalyzer(_data);
            var ema_data = analyser.getSMA();

            foreach (float val in ema_data)
                entries.Add(new ChartEntry(val) { Color = SKColor.Parse("#20f") });

            return new LineChart() { Entries = entries, PointSize = 1, LineMode = LineMode.Straight, AnimationDuration = new TimeSpan(0, 0, 0, 0, 500), MaxValue = MaxValue + 1, MinValue = MinValue - 1 };
        }

        public Chart UpdateBBLine_1Chart()
        {
            List<ChartEntry> entries = new List<ChartEntry>();

            ICollection<float> _data = (ICollection<float>)Data;

            DataAnalyzer analyser = new DataAnalyzer(_data);
            var ema_data = analyser.BBLine1();

            foreach (float val in ema_data)
                entries.Add(new ChartEntry(val) { Color = SKColor.Parse("#ffc800") });

            return new LineChart() { Entries = entries, PointSize = 1, LineMode = LineMode.Straight, AnimationDuration = new TimeSpan(0, 0, 0, 0, 500), MaxValue = MaxValue + 1, MinValue = MinValue - 1 };
        }

        public Chart UpdateBBLine_2Chart()
        {
            List<ChartEntry> entries = new List<ChartEntry>();

            ICollection<float> _data = (ICollection<float>)Data;

            DataAnalyzer analyser = new DataAnalyzer(_data);
            var ema_data = analyser.BBLine2();

            foreach (float val in ema_data)
                entries.Add(new ChartEntry(val) { Color = SKColor.Parse("#b0f") });

            return new LineChart() { Entries = entries, PointSize = 1, LineMode = LineMode.Straight, AnimationDuration = new TimeSpan(0, 0, 0, 0, 500), MaxValue = MaxValue + 1, MinValue = MinValue - 1 };
        }
    }
}
