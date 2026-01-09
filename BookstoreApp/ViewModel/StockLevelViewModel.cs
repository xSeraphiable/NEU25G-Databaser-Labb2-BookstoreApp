using BookstoreApp.Commands;
using BookstoreApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class StockLevelViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;

        public DelegateCommand SaveStockLevelCommand { get; }
        public DelegateCommand CancelChangesCommand { get; }


        public async void SaveStock(object? args)
        {
            if (SelectedStore is null)
                return;

            var modifiedRows = StockLevel.Where(r => r.IsModified).ToList();
            if (!modifiedRows.Any())
                return;

            using var db = new BookstoreContext();

            foreach (var row in modifiedRows)
            {
                var entity = await db.StockLevels.FirstAsync(sl =>
                    sl.StoreId == SelectedStore.StoreId &&
                    sl.Isbn == row.Isbn);

                entity.Quantity = row.Quantity;
                entity.QuantityOrdered = row.QuantityOrdered;
            }

            await db.SaveChangesAsync();

            foreach (var row in modifiedRows)
                row.AcceptChanges();

            SaveStockLevelCommand.RaiseCanExecuteChanged();
        }

        public bool CanSaveStock(object? args)
        {
            return StockLevel.Any(r =>
         r.IsModified &&
         !r.HasErrors);

        }

        public StockLevelViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;

            SaveStockLevelCommand = new DelegateCommand(SaveStock, CanSaveStock);

            _mainWindowViewModel.PropertyChanged += async (_, e) =>
            {
                if (e.PropertyName == nameof(MainWindowViewModel.SelectedStore))
                {
                    RaisePropertyChanged(nameof(SelectedStore));
                    await LoadStockLevelAsync();
                }
            };

        }

        public ObservableCollection<Store> Stores => _mainWindowViewModel.Stores;

        public Store? SelectedStore
        {
            get => _mainWindowViewModel.SelectedStore;

        }

        private StockLevelRowViewModel _selectedRow;
        public StockLevelRowViewModel SelectedRow
        {
            get => _selectedRow;
            set
            {
                _selectedRow = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<StockLevelRowViewModel> StockLevel { get; } = new();

        public async Task LoadStockLevelAsync()
        {
            StockLevel.Clear();

            if (SelectedStore is null)
                return;

            using var db = new BookstoreContext();

            var items = await db.StockLevels
                .Where(sl => sl.StoreId == SelectedStore.StoreId)
                .Select(sl => new StockLevelRowViewModel(
                    sl.Isbn,
                    sl.IsbnNavigation.Title,
                    string.Join(", ",
                        sl.IsbnNavigation.Authors
                            .Select(a => a.FirstName + " " + a.Surname)),
                    sl.Quantity,
                    sl.QuantityOrdered,
                    sl.IsbnNavigation.SalesPrice
                ))
                .ToListAsync();

            foreach (var item in items)
            {
                item.OnModifiedChanged =
                    () => SaveStockLevelCommand.RaiseCanExecuteChanged();

                StockLevel.Add(item);
            }


        }
    }
}
