using BookstoreApp.Commands;
using BookstoreApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class AuthorDetailViewModel : ViewModelBase, IDataErrorInfo
    {

        private int _authorId;
        private string _firstName;
        private string _surname;
        private DateOnly? _dateOfBirth;

        //New author constructor
        public AuthorDetailViewModel()
        {
            IsNew = true;
            IsModified = false;

            SaveAuthorCommand = new DelegateCommand(SaveAuthorAsync);

           
        }

        //Edit author constructor
        public AuthorDetailViewModel(Author author)
        {
            IsNew = false;
            IsModified = false;

            SaveAuthorCommand = new DelegateCommand(SaveAuthorAsync);

            _authorId = author.AuthorId;
            FirstName = author.FirstName;
            Surname = author.Surname;
            DateOfBirth = author.DateOfBirth;

        }

        public bool IsNew { get; }
        public bool IsModified { get; private set; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnChanged();
            }
        }

        public DateOnly? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnChanged();
            }
        }

        public DelegateCommand SaveAuthorCommand { get; }

        public async void SaveAuthorAsync(object? args)
        {
            using var db = new BookstoreContext();

            Author author;

            if (HasErrors) return;

            if (IsNew)
            {
                author = new Author();
                db.Authors.Add(author);
            }
            else
            {
                var editAuthor = await db.Authors
                    .FirstOrDefaultAsync(a => a.AuthorId == _authorId);

                if (editAuthor == null)
                    return;

                author = editAuthor;
            }

            author.FirstName = FirstName;
            author.Surname = Surname;
            author.DateOfBirth = DateOfBirth;

            await db.SaveChangesAsync();
            RequestClose?.Invoke(true);
        }
      
        public string Error => string.Empty;
        public string this[string columnName] => Validate(columnName);
        private bool HasErrors =>
    !string.IsNullOrEmpty(this[nameof(FirstName)]) ||
    !string.IsNullOrEmpty(this[nameof(Surname)]) ||
    !string.IsNullOrEmpty(this[nameof(DateOfBirth)]);
        
        
        private string Validate(string propertyName)

        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.IsNullOrWhiteSpace(FirstName))
                        return "Förnamn måste anges";
                    if (FirstName.Length > 100)
                        return "Förnamn är för långt";
                    break;

                case nameof(Surname):
                    if (string.IsNullOrWhiteSpace(Surname))
                        return "Efternamn måste anges";
                    if (Surname.Length > 100)
                        return "Efternamn är för långt";
                    break;

                case nameof(DateOfBirth):
                    if (DateOfBirth.HasValue && DateOfBirth > DateOnly.FromDateTime(DateTime.Today))
                        return "Födelsedatum kan inte ligga i framtiden";
                    break;
            }

            return string.Empty;
        }

        private void OnChanged([CallerMemberName] string? name = null)
        {
            IsModified = true;
            RaisePropertyChanged(name);
            RaisePropertyChanged(nameof(IsModified));
        }

        public event Action<bool>? RequestClose;
        

    }
}
