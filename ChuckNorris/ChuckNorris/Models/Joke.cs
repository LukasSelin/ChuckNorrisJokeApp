using ChuckNorris.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChuckNorris.Models
{
    public partial class Joke
    {
        public string ID { get; set; }
        public string Value { get; set; }
        public bool IsFavorite { get; set; } = false;
        public Xamarin.Forms.ImageSource ImageSource { get; set; } = Icon.NonFavorite;

    }
}
