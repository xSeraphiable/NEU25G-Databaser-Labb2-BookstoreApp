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

        public DelegateCommand ShowBooksCommand { get; }
        public DelegateCommand ShowStockCommand { get; }
        public DelegateCommand ShowAuthorsCommand { get; }

        public MainWindowViewModel()
        {
            StockLevelViewModel = new StockLevelViewModel();
            BooksViewModel = new BooksViewModel();
            AuthorsViewModel = new AuthorsViewModel();

            CurrentView = StockLevelViewModel;

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
            if (StockLevelViewModel.SelectedStore != null)
            {
              _ = StockLevelViewModel.LoadStockLevelsAsync();
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


    }
}
