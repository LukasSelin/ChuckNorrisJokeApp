using System;
using System.Collections.Generic;
using ChuckNorris.ViewModels;
using ChuckNorris.Views;
using Xamarin.Forms;

namespace ChuckNorris
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(JokeDetailsPage), typeof(JokeDetailsPage));




        }

    }
}
