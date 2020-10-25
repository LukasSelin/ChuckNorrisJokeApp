using ChuckNorris.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ChuckNorris.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChuckNorris.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        SearchPageViewModel _viewModel;
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SearchPageViewModel();

        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            searchResults.ItemsSource = _viewModel.Jokes = new ObservableCollection<Joke>(await _viewModel.JokeRepository.GetFromSearch(searchBar.Text, _viewModel.SelectedFilter));
        }

        private async void DropdownFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (ChuckNorris.Controls.CustomPicker)sender;
            _viewModel.SelectedFilter = (string) picker.SelectedItem;
            SelectedFilter.Text = _viewModel.SelectedFilter;
            searchResults.ItemsSource = await _viewModel.JokeRepository.GetFromSearch(searchBar.Text, _viewModel.SelectedFilter);
        }
    }
}