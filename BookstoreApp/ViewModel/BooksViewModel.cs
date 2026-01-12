using BookstoreApp.Commands;
using BookstoreApp.Infrastructure;
using BookstoreApp.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookstoreApp.ViewModel
{
    internal class BooksViewModel : ViewModelBase
    {

        public BooksViewModel()
        {
            EditBookCommand = new DelegateCommand(EditBookAsync, CanEditBook);
            NewBookCommand = new DelegateCommand(NewBookAsync);
            DeleteBookCommand = new DelegateCommand(DeleteBookAsync, CanDeleteBook);

            Rows = new ObservableCollection<BookRowViewModel>();

            Load();
        }

        public ObservableCollection<BookRowViewModel> Rows { get; private set; }

        public DelegateCommand EditBookCommand { get; }
        public DelegateCommand NewBookCommand { get; }
        public DelegateCommand DeleteBookCommand { get; }


        private BookRowViewModel? _selectedBookRow;

        public BookRowViewModel? SelectedBookRow
        {
            get => _selectedBookRow;
            set
            {
                _selectedBookRow = value;
                RaisePropertyChanged();
                EditBookCommand.RaiseCanExecuteChanged();
                DeleteBookCommand.RaiseCanExecuteChanged();
            }
        }

        private async void Load()
        {
            await LoadBookRowsAsync();
        }



        public async void DeleteBookAsync(object? args)
        {
            if (SelectedBookRow is null)
                return;

            var result = MessageBox.Show(
                $"Är du säker på att du vill ta bort \"{SelectedBookRow.Title}\"?\nDetta går inte att ångra.",
                "Radera bok",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Cancel)
                return;

            using var db = new BookstoreContext();

            var book = await db.Books
                .FirstOrDefaultAsync(b => b.Isbn == SelectedBookRow.Isbn);

            if (book == null)
                return;

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            await LoadBookRowsAsync();

        }

        public bool CanDeleteBook(object? args)
        {
            return SelectedBookRow != null;
        }

        public async void EditBookAsync(object? args)
        {
            if (SelectedBookRow is null)
                return;

            using var db = new BookstoreContext();

            var book = await db.Books
                .Include(b => b.Category)
                .FirstAsync(b => b.Isbn == SelectedBookRow.Isbn);

            if (book == null)
                return;

            var vm = new BookDetailViewModel(book);

            var dialog = new AddEditBookWindow
            {
                DataContext = vm
            };

            if (dialog.ShowDialog() == true)
            {
                await LoadBookRowsAsync();
            }

        }

        private bool CanEditBook(object? args)
        {
            return SelectedBookRow != null;
        }

        public async void NewBookAsync(object? args) //TODO: gör klart
        {
            var vm = new BookDetailViewModel();

            var dialog = new AddEditBookWindow
            {
                DataContext = vm
            };

            if (dialog.ShowDialog() == true)
            {
                //SaveBook(vm);
                await LoadBookRowsAsync();
            }
        }

        public async Task LoadBookRowsAsync()
        {

            using var db = new BookstoreContext();

            var books = await db.Books
                .Select(b => new BookRowViewModel(
                    b.Isbn,
                    b.Title,
                    b.SalesPrice,
                    b.ReleaseDate
                ))
                .ToListAsync();

            Rows.Clear();

            foreach (var book in books)
            {
                Rows.Add(book);
            }
        }

    }

}
