using BookstoreApp.Commands;
using BookstoreApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookstoreApp.ViewModel
{
    internal class StockLevelViewModel : ViewModelBase
    {
     
        public DelegateCommand SaveStockLevelCommand { get; }
        public DelegateCommand CancelStockLevelCommand { get; }

        public StockLevelViewModel()
        {
         
            SaveStockLevelCommand = new DelegateCommand(SaveStock, CanSaveStock);
            CancelStockLevelCommand = new DelegateCommand(CancelChanges, CanCancel);

        }
        private void CancelChanges(object? args)
        {
            foreach (var row in StockLevel)
                row.Reset();
        }
        private bool CanCancel(object? args)
        {
               return ModifiedCount > 0;
        }
        public async void SaveStock(object? args)
        {
            if (SelectedStore is null)
                return;

            if (StockLevel.Any(r => r.HasErrors))
            {
                MessageBox.Show(
                    "Det finns ogiltiga värden. Kontrollera lagersaldot innan du sparar.",
                    "Kan inte spara",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

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
            await LoadStockLevelsAsync();

            SaveStockLevelCommand.RaiseCanExecuteChanged();
        }

        public bool CanSaveStock(object? args)
        {

            return StockLevel.Any(r => r.IsModified);

        }

        public int ModifiedCount =>
    StockLevel.Count(r => r.IsModified);
        public ObservableCollection<StockLevelRowViewModel> StockLevel { get; } = new();

        public ObservableCollection<Store> Stores { get; } = new();

        private Store? _selectedStore;
        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                if (_selectedStore == value)
                    return;

                _selectedStore = value;
                RaisePropertyChanged();

                _ = LoadStockLevelsAsync();
            }
        }

        private StockLevelRowViewModel? _selectedRow;
        public StockLevelRowViewModel? SelectedRow
        {
            get => _selectedRow;
            set
            {
                _selectedRow = value;
                RaisePropertyChanged();
            }
        }

        private bool _isInitialized;

        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            await LoadStoresAsync();
        }

        public async Task LoadStoresAsync()
        {
            if (Stores.Count > 0)
                return; 

            try
            {
                using var db = new BookstoreContext();
                var stores = await db.Stores.ToListAsync();

                foreach (var store in stores)
                    Stores.Add(store);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kunde inte ladda butiker.\n\n" + ex.Message,
                    "Fel",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public async Task LoadStockLevelsAsync()
        {
            try
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

                item.OnModifiedChanged = () =>
                {
                    SaveStockLevelCommand.RaiseCanExecuteChanged();
                    CancelStockLevelCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(nameof(ModifiedCount));
                };

                StockLevel.Add(item);
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kunde inte ladda lagersaldo.\n\n" + ex.Message,
                    "Fel vid inläsning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }
    }
}
