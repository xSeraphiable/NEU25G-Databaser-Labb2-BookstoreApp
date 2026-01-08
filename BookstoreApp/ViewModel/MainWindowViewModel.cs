using BookstoreApp.Infrastructure;
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
        public ObservableCollection<Store> Stores { get; set; }

        private Store _selectedStore;

        public Store SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                StockLevelViewModel.LoadStockLevel();
                RaisePropertyChanged();
                RaisePropertyChanged("StockLevel");
             
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
            LoadStores(); //TODO: ändra till asynkront
        }
        private void LoadStores()
        {
            using var db = new BookstoreContext();

            Stores = new ObservableCollection<Store>(
           db.Stores.ToList());
     
        }

    }
}
