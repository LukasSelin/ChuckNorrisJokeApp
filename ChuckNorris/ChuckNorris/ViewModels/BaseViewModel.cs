using ChuckNorris.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ChuckNorris.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public JokeRepository JokeRepository;
        public FavoriteService FavoriteService;

        public BaseViewModel()
        {
            JokeRepository = DependencyService.Get<JokeRepository>();
            FavoriteService = DependencyService.Get<FavoriteService>();
        }

    protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
