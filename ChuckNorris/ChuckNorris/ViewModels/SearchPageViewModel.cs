using ChuckNorris.Models;
using ChuckNorris.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using ChuckNorris.Views;

namespace ChuckNorris.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        public List<Joke> _Jokes
        {
            get
            {
                return Jokes.ToList();
            }
        }
        public ObservableCollection<Joke> Jokes { get; set; }
        public ObservableCollection<string> Filters { get; set; }
        public string SelectedFilter { get; set; }
        public Command ToggleIconCommand { get; }
        public Command ItemTapped { get; }
        private ImageSource imageSource;
        public ImageSource ImageSource
        {
            get
            {
                if (imageSource == null)
                {
                    imageSource = Icon.NonFavorite;
                }
                return imageSource;
            }
            set
            {
                SetProperty(ref imageSource, value);
            }
        }
        public SearchPageViewModel()
        {
            Title = "Search";
            ToggleIconCommand = new Command<Joke>(ToggleIcon);
            ItemTapped = new Command<Joke>(ItemSelected);
            ItemTapped = new Command<Joke>(j => (j).ToString());

            Filters = new ObservableCollection<string>() { "No filter", "Favorite" };
        }

        public void ToggleIcon(Joke joke)
        {
            if (joke == null)
                return;

            joke = FavoriteService.ToggleFavorite(joke);

            var jokeIndex = _Jokes.FindIndex(j => j.ID == joke.ID);
            Jokes[jokeIndex] = joke;
        }
        public async void ItemSelected(Joke joke)
        {
            joke.Value = joke.Value.Replace('"', '\'');
            var jsonJoke = Newtonsoft.Json.JsonConvert.SerializeObject(joke);
            await Shell.Current.GoToAsync($"{ nameof(JokeDetailsPage)}?{ nameof(JokeDetailsModelView.JokeJson)}={ jsonJoke }");

        }
    }
}
