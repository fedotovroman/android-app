using MoneyApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoneyApp.ViewModels
{
    class ConvertViewModel : BaseViewModel
    {
        //
        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private IList<Quote> quotes;
        public IList<Quote> Quotes
        {
            get => quotes;
            set => SetProperty(ref quotes, value);
        }

        //
        public ConvertViewModel()
        {
            Quotes = new List<Quote>(); 
            Title = "Конвертер";

            LoadItems();
        }

        void LoadItems()
        {
            IEnumerable<Quote> tmp = DataStore.GetQuotes("");

            foreach(Quote item in tmp)
            {
                Quotes.Add(item);
            }
        }
    }
}
