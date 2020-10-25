using ChuckNorris.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace ChuckNorris
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<JokeRepository>();
            DependencyService.Register<FavoriteService>();


            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
