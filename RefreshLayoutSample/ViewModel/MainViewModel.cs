using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RefreshLayoutSample.Model;
using Xamarin.Forms;

namespace RefreshLayoutSample.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Property

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (SetProperty(ref isBusy, value))
                    IsNotBusy = !isBusy;
            }
        }

        bool isNotBusy = true;

        public bool IsNotBusy
        {
            get => isNotBusy;
            set
            {
                if (SetProperty(ref isNotBusy, value))
                    IsBusy = !isNotBusy;
            }
        }

        string _data;
        public string Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        string _carregou;
        public string Carregou
        {
            get => _carregou;
            set => SetProperty(ref _carregou, value);
        }

        Command forceRefreshCommand;
        public Command ForceRefreshCommand =>
        forceRefreshCommand ?? (forceRefreshCommand = new Command(async () => await ExecuteForceRefreshCommandAsync()));

        private async Task ExecuteForceRefreshCommandAsync()
        {
            IsBusy = true;

            await Task.Delay(6000);

            Users.Add(new User
            {
                Name = "Bertuzzi"
            });

            Users.Add(new User
            {
                Name = "Bruna"
            });

            Users.Add(new User
            {
                Name = "Polly"
            });

            Users.Add(new User
            {
                Name = "Rodolfo"
            });

            Users.Add(new User
            {
                Name = "Lester"
            });

            Data = DateTime.Now.ToString("dd/MM/yyy");
            Carregou = "Carregou";

            IsBusy = false;
        }


        public ObservableCollection<User> Users { get; }

        private int _rating;
        public int Rating
        {
            get { return _rating; }
            set => SetProperty(ref _rating, value);
        }

        public MainViewModel()
        {
            Users = new ObservableCollection<User>();

           

        }


    }
}
