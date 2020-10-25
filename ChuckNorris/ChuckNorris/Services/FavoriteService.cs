using ChuckNorris.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChuckNorris.Services
{
    static class Icon
    {
        public static ImageSource Favorite
        {
            get
            {
                return ImageSource.FromFile("ic_star_black_18dp.png");
            }
        }
        public static ImageSource NonFavorite
        {
            get
            {
                return ImageSource.FromFile("ic_star_border_black_18dp.png");
            }
        }
    }
    interface IFavoriteService
    {
        bool IsFavorite(Joke joke);
        Joke ToggleFavorite(Joke joke);
        ImageSource GetIcon(Joke joke);
    }
    public class FavoriteService : IFavoriteService
    {
        private List<Joke> _favoriteJokes;
        public FavoriteService()
        {
            _favoriteJokes = new List<Joke>();
        }
       
        public bool IsFavorite(Joke joke)
        {
            return _favoriteJokes.Exists(fav => fav.ID == joke.ID);
        }
        public Joke ToggleFavorite(Joke joke)
        {
            if(!joke.IsFavorite)
            {
                return AddToFavorite(joke);
            }
            else
            {
                return RemoveFromFavorite(joke);
            }
        }
        public ImageSource GetIcon(Joke joke)
        {
            if (joke == null)
            {
                return Icon.NonFavorite;
            }
            var iconSource = joke.IsFavorite ? Icon.Favorite : Icon.NonFavorite;
            return iconSource;
        }
        #region Private methods
        private Joke AddToFavorite(Joke joke)
        {

            if(!IsFavorite(joke))
            {
                _favoriteJokes.Add(joke);
            }

            joke.IsFavorite = true;
            joke.ImageSource = Icon.Favorite;
            return joke;
        }
        private Joke RemoveFromFavorite(Joke joke)
        {
            joke.IsFavorite = false;
            joke.ImageSource = Icon.NonFavorite;

            var favJoke = _favoriteJokes.Find(fav => fav.ID == joke.ID);
            _favoriteJokes.Remove(favJoke);

            return joke;
        }
        #endregion
    }
}
