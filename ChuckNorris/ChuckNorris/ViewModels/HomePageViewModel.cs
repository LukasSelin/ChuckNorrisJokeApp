using ChuckNorris.Models;
using ChuckNorris.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChuckNorris.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        public string SelectedCategory;
        public HomePageViewModel()
        {
            Title = "Homepage";
        }
    }
}
