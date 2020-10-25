using System;
using System.Collections.Generic;
using System.Text;
using ChuckNorris.Models;
using Xamarin.Forms;

namespace ChuckNorris.ViewModels
{
    [QueryProperty(nameof(JokeJson), nameof(JokeJson))]
    class JokeDetailsModelView : BaseViewModel
    {
        private Joke _joke;
        public JokeDetailsModelView()
        {
            Title = "Details";
        }
            

        public Joke Joke
        {
            get
            {
                return _joke;
            }
            set
            {
                value.IsFavorite = FavoriteService.IsFavorite(value);
                SetProperty(ref _joke, value);
            }
        }
        public string JokeJson
        {
            set
            {
                var converted = Uri.UnescapeDataString(value);
                var joke = Newtonsoft.Json.JsonConvert.DeserializeObject<Joke>(converted);
                Joke = joke;
            }
        }
    }
}

