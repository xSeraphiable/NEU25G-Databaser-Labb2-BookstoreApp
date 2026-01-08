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

        public StockLevelViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            LoadStockLevel();
        }

        public ObservableCollection<Store> Stores => _mainWindowViewModel.Stores;

        public Store SelectedStore
        {
            get => _mainWindowViewModel.SelectedStore;
            set
            {
                _mainWindowViewModel.SelectedStore = value;
                RaisePropertyChanged();
                LoadStockLevel();
            }
        }

        public ObservableCollection<StockLevelOverview> StockLevel { get; set; }

        public void LoadStockLevel()
        {
            
            // 0) Om ingen butik är vald -> visa tom tabell
            if (SelectedStore is null)
            {
                StockLevel ??= new ObservableCollection<StockLevelOverview>();
                StockLevel.Clear();
                return;
            }

            var storeId = SelectedStore.StoreId;

            using var db = new BookstoreContext();

            // 1) Hämta rådata (SQL-vänligt)
            var raw = db.StockLevels
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
                .ToList();

            // 2) Bygg tabellrader i C# (string.Join här)
            var items = raw.Select(x => new StockLevelOverview
            {
                Isbn = x.Isbn,
                Title = x.Title,
                Author = string.Join(", ", x.Authors.Select(a => $"{a.FirstName} {a.Surname}")),
                Quantity = x.Quantity,
                QuantityOrdered = x.QuantityOrdered,
                SalesPrice = x.SalesPrice
            }).ToList();

            // 3) Uppdatera ObservableCollection (UI uppdateras)
            StockLevel ??= new ObservableCollection<StockLevelOverview>();
            StockLevel.Clear();
            foreach (var item in items)
                StockLevel.Add(item);

            //StockLevel = new ObservableCollection<StockLevelOverview>(
            //    db.StockLevels
            //    .Where(s => s.Store.Street == SelectedStore.Street)
            //    .Select(s => new StockLevelOverview()
            //    {
            //        Isbn = s.Isbn,
            //        Title = s.IsbnNavigation.Title,
            //        Author = string.Join(", ", s.IsbnNavigation.Authors
            //                       .Select(a => $"{a.FirstName} {a.Surname}")),
            //        Quantity = s.Quantity,
            //        QuantityOrdered = s.QuantityOrdered,
            //        SalesPrice = s.IsbnNavigation.SalesPrice

            //    }).ToList());
        }
    }
}
