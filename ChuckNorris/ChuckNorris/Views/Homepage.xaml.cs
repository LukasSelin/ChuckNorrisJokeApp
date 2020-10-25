using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using ChuckNorris.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace ChuckNorris.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomePageViewModel _viewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new HomePageViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            categoriesList.SelectedItem = null;

            var categories = await _viewModel.JokeRepository.GetCategories();
            categoriesList.ItemsSource = categories;
        }
        private IEnumerable<string> FormatStrings(IEnumerable<string> strings)
        {
            List<string> returnStrings = strings.ToList();
            for (int i = 0; i < returnStrings.Count(); i++)
            {
                var charToUpper = Char.ToUpper(returnStrings[i][0]);
                var tempstring = charToUpper + returnStrings[i].Substring(1);
                returnStrings[i] = tempstring;
            }
            return returnStrings;
        }

        private async void categoriesList_ItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.Count != 0)
            {
                var category = (string) e.CurrentSelection.FirstOrDefault();

                var joke = await _viewModel.JokeRepository.GetFromCategory(category);
                var jokeJson = Newtonsoft.Json.JsonConvert.SerializeObject(joke);
                await Shell.Current.GoToAsync($"{ nameof(JokeDetailsPage)}?{ nameof(JokeDetailsModelView.JokeJson)}={ jokeJson }");
            }
        }
    }
}