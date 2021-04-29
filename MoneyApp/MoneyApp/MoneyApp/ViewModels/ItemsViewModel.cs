using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MoneyApp.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Xml;
using MoneyApp.View;

namespace MoneyApp.ViewModels
{
    class ItemsViewModel : INotifyPropertyChanged
    {
        //Свойства INotify
        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private Quote selected_quote;
        public Quote SelectedQuote 
        {
            get => selected_quote;
            set
            {
                SetProperty(ref selected_quote, value);
                OnSelected(value);
            }
        }

        //Поля
        public ObservableCollection<Quote> Quotes { get; }
        public Command LoadCommand { get; }
        public Command<Quote> SelectItemCommand { get; }


        //Конструктор
        public ItemsViewModel()
        {
            Quotes = new ObservableCollection<Quote>();

            IsBusy = false;
            LoadCommand = new Command(LoadItems);
            SelectItemCommand = new Command<Quote>(OnSelected);

            LoadItems();
        }

        //Работа с квотами
        void LoadItems()
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Quotes.Clear();
                    XmlDocument document = new XmlDocument();
                    document.Load("https://www.cbr-xml-daily.ru/daily.xml");
                    XmlNode root = document.DocumentElement;
                    XmlNodeList nodes = root.ChildNodes;

                    foreach (XmlNode node in nodes)
                    {
                        XmlAttribute attr = node.Attributes[0];
                        string id = attr.Value;
                        string name = node.SelectSingleNode("./Name").InnerText;
                        string char_code = node.SelectSingleNode("./CharCode").InnerText;
                        int nominal = Convert.ToInt32(node.SelectSingleNode("./Nominal").InnerText);
                        decimal value = Convert.ToDecimal(node.SelectSingleNode("./Value").InnerText);
                        string image = char_code + ".png";

                        if(char_code != "XDR")
                            Quotes.Add(new Quote() { Id = id, Name = name, CharCode = char_code, Nominal = nominal, Value = value, Image = image });
                    }
                }
                else
                {
                    Quotes.Clear();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        //
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedQuote = null;
        }

        //
        async void OnSelected(Quote item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(nameof(ItemDetailsPage));
        }

        //Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
