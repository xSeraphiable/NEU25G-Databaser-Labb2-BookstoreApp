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
        private readonly MainWindowViewModel? _mainWindowViewModel;
        public BooksViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            LoadBookRows();
        }

        public ObservableCollection<BookRowViewModel> Rows { get; private set; }
        public void LoadBookRows() //TODO: ändra till async 
        {
            using var db = new BookstoreContext();

            Rows = new ObservableCollection<BookRowViewModel>( db.Books
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
