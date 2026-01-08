using BookstoreApp.Infrastructure;
using BookstoreApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class StockLevelViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;

        public StockLevelViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            StockLevel = new ObservableCollection<StockLevelOverview>();
        }

        public ObservableCollection<Store> Stores => _mainWindowViewModel.Stores;

        public Store? SelectedStore
        {
            get => _mainWindowViewModel.SelectedStore;
            set
            {
                _mainWindowViewModel.SelectedStore = value;
                RaisePropertyChanged();
                _ = LoadStockLevelAsync();
            }
        }

        public ObservableCollection<StockLevelOverview> StockLevel { get; set; }

        public async Task LoadStockLevelAsync()
        {
            if (SelectedStore is null)
            {
                StockLevel ??= new ObservableCollection<StockLevelOverview>();
                StockLevel.Clear();
                return;
            }

            var storeId = SelectedStore.StoreId;

            using var db = new BookstoreContext();

            var raw = await db.StockLevels
                .Where(sl => sl.StoreId == storeId)
                .Select(sl => new
                {
                    sl.Isbn,
                    Title = sl.IsbnNavigation.Title,
                    Authors = sl.IsbnNavigation.Authors.Select(a => new { a.FirstName, a.Surname }).ToList(),
                    sl.Quantity,
                    sl.QuantityOrdered,
                    SalesPrice = sl.IsbnNavigation.SalesPrice
                })
                .ToListAsync();

            var items = raw.Select(x => new StockLevelOverview
            {
                Isbn = x.Isbn,
                Title = x.Title,
                Author = string.Join(", ", x.Authors.Select(a => $"{a.FirstName} {a.Surname}")),
                Quantity = x.Quantity,
                QuantityOrdered = x.QuantityOrdered,
                SalesPrice = x.SalesPrice
            }).ToList();

            StockLevel ??= new ObservableCollection<StockLevelOverview>();
            StockLevel.Clear();
            foreach (var item in items)
                StockLevel.Add(item);
        }
    }
}
