using ChuckNorris.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChuckNorris.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JokeDetailsPage : ContentPage
    {
        JokeDetailsModelView _viewModel;
        public JokeDetailsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new JokeDetailsModelView();
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            _viewModel.Joke = _viewModel.FavoriteService.ToggleFavorite(_viewModel.Joke);
            FavoriteIcon.IconImageSource = _viewModel.Joke.ImageSource;
        }
    }
}