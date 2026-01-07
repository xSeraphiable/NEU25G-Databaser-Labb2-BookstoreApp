using BookstoreApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class StockLevelViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;

        public StockLevelViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public ObservableCollection<Store> Stores => _mainWindowViewModel.Stores;

        //public Store SelectedStore
        //{
        //    get => _mainWindowViewModel.SelectedStore;
        //    set => _mainWindowViewModel.SelectedStore = value;
        //}
    }
}
