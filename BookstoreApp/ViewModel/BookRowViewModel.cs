using BookstoreApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class BookRowViewModel : ViewModelBase
    {

        public Book Book { get; }

        public BookRowViewModel(Book book)
        {
            Book = book;
        }

        public string Isbn => Book.Isbn;
        public string Title => Book.Title;
        public decimal Price => Book.SalesPrice;
        public DateOnly? ReleaseDate => Book.ReleaseDate;

        public string AuthorsText =>
            string.Join(", ",
                Book.Authors.Select(a => $"{a.FirstName} {a.Surname}"));

    }
}
