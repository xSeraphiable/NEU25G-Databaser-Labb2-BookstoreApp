using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class BookRowViewModel : ViewModelBase
    {
        public BookRowViewModel(string isbn, string title, decimal price, DateOnly? releaseDate)
        {
            Isbn = isbn;
            Title = title;
            Price = price;
            ReleaseDate = releaseDate;
        }
        public string Isbn { get; }
        public string Title { get; }
        public decimal Price { get; }
        public DateOnly? ReleaseDate { get; }
    }
}
