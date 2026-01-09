using BookstoreApp.Commands;
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
        public DelegateCommand SelectStoreCommand { get; }

        public bool CanSelectPack(object? args)
        {
            return args is Store;
        }

        public void SelectPack(object? args)
        {
            if (args is Store store)
            {
                SelectedStore = store;
            }
        }

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

            SelectStoreCommand = new DelegateCommand(SelectPack, CanSelectPack);
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
