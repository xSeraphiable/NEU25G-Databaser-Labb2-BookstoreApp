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
        public DelegateCommand ShowBooksCommand { get; }
        public DelegateCommand ShowStockCommand { get; }
        public DelegateCommand ShowAuthorsCommand { get; }

        public MainWindowViewModel()
        {
            StockLevelViewModel = new StockLevelViewModel(this);
            BooksViewModel = new BooksViewModel();
            AuthorsViewModel = new AuthorsViewModel();

            CurrentView = StockLevelViewModel;

            _ = LoadStoresAsync();

            SelectStoreCommand = new DelegateCommand(SelectStore, CanSelectStore);
            ShowBooksCommand = new DelegateCommand(ShowBooks);
            ShowStockCommand = new DelegateCommand(ShowStock);
            ShowAuthorsCommand = new DelegateCommand(ShowAuthors);
        }
        public StockLevelViewModel StockLevelViewModel { get; }
        public BooksViewModel BooksViewModel { get; }
        public AuthorsViewModel AuthorsViewModel { get; }

       
        private void ShowAuthors(object? args)
        {
            AuthorsViewModel.SelectedAuthorRow = null;
            CurrentView = AuthorsViewModel;
        }
        private void ShowBooks(object? args)
        {
            BooksViewModel.SelectedBookRow = null;
            CurrentView = BooksViewModel;
        }

        private void ShowStock(object? args)
        {
            CurrentView = StockLevelViewModel;
        }

        public bool CanSelectStore(object? args)
        {
            return args is Store;
        }

        public void SelectStore(object? args)
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
