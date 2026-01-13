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


        public DelegateCommand SaveAuthorCommand { get; }

        public event Action<bool>? RequestClose;

        public async void SaveAuthorAsync(object? args)
        {
            using var db = new BookstoreContext();

            Author author;

            if (IsNew)
            {
                author = new Author();
                db.Authors.Add(author);
            }
            else
            {
                author = await db.Authors
                    .FirstOrDefaultAsync(a => a.AuthorId == _authorId);

                if (author == null)
                    return;
            }

            author.FirstName = FirstName;
            author.Surname = Surname;
            author.DateOfBirth = DateOfBirth;

            await db.SaveChangesAsync();
            RequestClose?.Invoke(true);
        }

        private void OnChanged([CallerMemberName] string? name = null)
        {
            IsModified = true;
            RaisePropertyChanged(name);
            RaisePropertyChanged(nameof(IsModified));
        }

        public bool IsNew { get; }
        public bool IsModified { get; private set; }

        private int _authorId;

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnChanged();
            }
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnChanged();
            }
        }

        private DateOnly? _dateOfBirth;
        public DateOnly? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnChanged();
            }
        }
        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();
    }
}
