using BookstoreApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Store> Stores { get; } = new();

        private Store? _selectedStore;
        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                RaisePropertyChanged();
            }
        }

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                RaisePropertyChanged();
            }
        }

        public StockLevelViewModel StockLevelViewModel { get; set; }

        public MainWindowViewModel()
        {
            StockLevelViewModel = new StockLevelViewModel(this);
            CurrentView = StockLevelViewModel;

            _ = LoadStoresAsync();
        }
        private async Task LoadStoresAsync()
        {
            using var db = new BookstoreContext();

            var stores = await db.Stores.ToListAsync();

            Stores.Clear();
            foreach (var s in stores)
                Stores.Add(s);
        }

    }
}
