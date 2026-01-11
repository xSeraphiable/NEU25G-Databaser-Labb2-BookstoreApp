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

namespace BookstoreApp.ViewModel
{
    internal class BooksViewModel : ViewModelBase
    {

        public BooksViewModel()
        {
            EditBookCommand = new DelegateCommand(EditBook);
            NewBookCommand = new DelegateCommand(NewBook);
            LoadBookRows();
        }

        public ObservableCollection<BookRowViewModel> Rows { get; private set; }

        public DelegateCommand EditBookCommand { get; }
        public DelegateCommand NewBookCommand { get; }

        public void EditBook(object? args)
        {
            if (SelectedBookRow is null)
                return;

            using var db = new BookstoreContext();
            var book = db.Books.First(b => b.Isbn == SelectedBookRow.Isbn);

            var vm = new BookDetailViewModel(book);
            var dialog = new AddEditBookWindow
            {
                DataContext = vm
            };

            if (dialog.ShowDialog() == true)
            {
                //SaveBook(vm);
                LoadBookRows();
            }
        }

        public void NewBook(object? args)
        {
            var vm = new BookDetailViewModel();
            var dialog = new AddEditBookWindow
            {
                DataContext = vm
            };

            if (dialog.ShowDialog() == true)
            {
                //SaveBook(vm);
                LoadBookRows();
            }
        }


        private BookRowViewModel? _selectedBookRow;

        public BookRowViewModel? SelectedBookRow
        {
            get => _selectedBookRow;
            set
            {
                _selectedBookRow = value;
                RaisePropertyChanged();
            }
        }



        public void LoadBookRows() //TODO: ändra till async 
        {
            using var db = new BookstoreContext();

            Rows = new ObservableCollection<BookRowViewModel>(db.Books
                .Select(b => new BookRowViewModel(
                    b.Isbn,
                    b.Title,
                    b.SalesPrice,
                    b.ReleaseDate
                ))
                .ToList());

        }
    }
}
