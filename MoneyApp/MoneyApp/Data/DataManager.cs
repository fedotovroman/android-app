using MoneyApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using Xamarin.Essentials;
using System.Linq;
using System.IO;
using System.Reflection;

namespace MoneyApp.Data
{
    public class DataManager
    {
        //Катировки
        private ObservableCollection<Quote> quotes;
        private ObservableCollection<Quote> favorite_quotes;
        private List<string> FavId;

        private ObservableCollection<Quote> Quotes 
        {
            get
            {
                LoadItems();
                return quotes;
            }
        }

        private ObservableCollection<Quote> FavoriteQuotes 
        {
            get
            {
                LoadItems();
                return favorite_quotes;
            }
        }

        //Конструктор
        public DataManager()
        {
            quotes = new ObservableCollection<Quote>();
            favorite_quotes = new ObservableCollection<Quote>();
        }

        //Загрузка катировок
        void LoadItems()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                quotes.Clear();
                favorite_quotes.Clear();

                //Загрузка через localdata
                FavId = new List<string>();
                string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "favorite_quotes.txt");
                if (!File.Exists(filename))
                {
                    FileStream fs = File.Create(filename);
                    fs.Dispose();
                }
                using (StreamReader reader = new StreamReader(filename))
                {
                    while (!reader.EndOfStream)
                    {
                        FavId.Add(reader.ReadLine());
                    }
                }

                //Ответ от сервера
                XmlDocument document = new XmlDocument();
                document.Load("https://www.cbr-xml-daily.ru/daily.xml");
                XmlNode root = document.DocumentElement;
                XmlNodeList nodes = root.ChildNodes;
                    
                //Запись данных в коллекции
                foreach (XmlNode node in nodes)
                {
                    XmlAttribute attr = node.Attributes[0];
                    string id = attr.Value;
                    string name = node.SelectSingleNode("./Name").InnerText;
                    string char_code = node.SelectSingleNode("./CharCode").InnerText;
                    int nominal = Convert.ToInt32(node.SelectSingleNode("./Nominal").InnerText);
                    decimal value = Convert.ToDecimal(node.SelectSingleNode("./Value").InnerText);
                    string image = id + ".png";
                    bool is_favorite = false;

                    if (char_code != "XDR")
                    {
                        if (FavId.Contains(id))
                        {
                            is_favorite = true;
                            favorite_quotes.Add(new Quote() { Id = id, Name = name, CharCode = char_code, Nominal = nominal, Value = value, Image = image, IsFavorite = is_favorite });
                        }
                        quotes.Add(new Quote() { Id = id, Name = name, CharCode = char_code, Nominal = nominal, Value = value, Image = image, IsFavorite = is_favorite });
                    }  
                }
            }
            else
            {
                quotes.Clear();
                favorite_quotes.Clear();
            }
        }

        //
        public void AddFavItem(string id)
        {
            if (!FavId.Contains(id))
            {
                FavId.Add(id);
                string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "favorite_quotes.txt");
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    foreach (string item in FavId)
                        writer.WriteLine(item);
                }
            }
        }

        public void RemoveFavItem(string id)
        {
            if (FavId.Contains(id))
            {
                FavId.Remove(id);
                string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "favorite_quotes.txt");
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    foreach (string item in FavId)
                        writer.WriteLine(item);
                }
            }
        }

        //
        public Quote GetQuote(string id)
        {
            Quote item = Quotes.FirstOrDefault(q => q.Id == id);

            if (item != null) 
            {
                return item;
            }
            else
            {
                return new Quote();
            }
        }

        public IEnumerable<Quote> GetQuotes(string name)
        {
            return Quotes.Where(q => q.Name.ToLower().Contains(name.ToLower()));
        }

        public IEnumerable<Quote> GetFavoriteQuotes(string name)
        {
            return FavoriteQuotes.Where(q => q.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
