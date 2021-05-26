using MoneyApp.Models;
using MoneyApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MoneyApp.ViewModels
{
    class FavoriteItemsViewModel : BaseViewModel
    {
        //Свойства INotify
        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
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

        private string search_string;
        public string SearchString
        {
            get => search_string;
            set => SetProperty(ref search_string, value);
        }

        private List<Quote> quotes;
        public List<Quote> Quotes
        {
            get => quotes;
            set => SetProperty(ref quotes, value);
        }

        //Свойства
        public string Title { get; }

        //Комманды
        public Command LoadCommand { get; }
        public Command<Quote> SelectItemCommand { get; }

        //Конструктор
        public FavoriteItemsViewModel()
        {
            Quotes = new List<Quote>();
            IsBusy = false;
            Title = "Котировки";
            SearchString = "";

            LoadCommand = new Command(LoadItems);
            SelectItemCommand = new Command<Quote>(OnSelected);
        }

        //Работа с квотами
        public void LoadItems()
        {
            IsBusy = true;

            if (SearchString == "" || SearchString == null)
                Quotes = DataStore.GetFavoriteQuotes("").ToList();
            else
                Quotes = DataStore.GetFavoriteQuotes(SearchString).ToList();

            IsBusy = false;
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

            await Shell.Current.GoToAsync($"{nameof(ItemDetailsPage)}?{nameof(ItemDetailsViewModel.ItemId)}={item.Id}", true);
        }
    }
}
