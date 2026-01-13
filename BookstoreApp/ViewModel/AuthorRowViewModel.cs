using BookstoreApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class AuthorRowViewModel : ViewModelBase
    {
        public Author Author { get; }

        public AuthorRowViewModel(Author author)
        {
            Author = author;
        }

        public int AuthorId => Author.AuthorId;

        public DateOnly? DateOfBirth => Author.DateOfBirth;

        public string FullName => $"{Author.FirstName} {Author.Surname}";

        public int BookCount => Author.Isbns.Count;

        public string BookTitlesText =>
    Author.Isbns.Any()
        ? string.Join(", ", Author.Isbns.Select(b => b.Title))
        : "Inga böcker";

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged();
                }
            }
        }
    }

}
