using BookstoreApp.Commands;
using BookstoreApp.Infrastructure;
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
            SelectBookCommand = new DelegateCommand(SelectBook, CanSelectBook);
            LoadBookRows();
        }

        public ObservableCollection<BookRowViewModel> Rows { get; private set; }

        public DelegateCommand SelectBookCommand { get; }

        public void SelectBook(object? args)
        {
            using var db = new BookstoreContext();

            var book = args as BookRowViewModel;

            //SelectedBook = new BookDetailViewModel(
            //    db.Books.Select(b => b.Isbn).
            //    Where(b => book.Isbn == ))
        }
        public bool CanSelectBook(object? args)
        {
            return args is BookRowViewModel b;
        }

        private Book _selectedBook;

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
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
