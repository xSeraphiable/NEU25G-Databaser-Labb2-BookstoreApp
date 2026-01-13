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
    internal class AuthorsViewModel : ViewModelBase
    {

        public AuthorsViewModel()
        {
            AuthorRows = new ObservableCollection<AuthorRowViewModel>();
            _ = LoadAuthorRowsAsync();

            NewAuthorCommand = new DelegateCommand(NewAuthorAsync);
            EditAuthorCommand = new DelegateCommand(EditAuthor, CanEditAuthor);
            DeleteAuthorCommand = new DelegateCommand(DeleteAuthor, CanDeleteAuthor);
        }

        public ObservableCollection<AuthorRowViewModel> AuthorRows { get; private set; }

        public DelegateCommand EditAuthorCommand { get; }
        public DelegateCommand DeleteAuthorCommand { get; }
        public DelegateCommand NewAuthorCommand { get; }

        private AuthorRowViewModel? _selectedAuthorRow;
        public AuthorRowViewModel? SelectedAuthorRow
        {
            get => _selectedAuthorRow;
            set
            {
                _selectedAuthorRow = value;
                RaisePropertyChanged();
                EditAuthorCommand.RaiseCanExecuteChanged();
                DeleteAuthorCommand.RaiseCanExecuteChanged();
            }
        }

        public async Task LoadAuthorRowsAsync()
        {

            using var db = new BookstoreContext();

            var authors = await db.Authors
                .Include(a => a.Isbns)
                .ToListAsync();

            AuthorRows.Clear();

            foreach (var a in authors)
            {
                AuthorRows.Add(new AuthorRowViewModel(a));
            }
        }

        public async void NewAuthorAsync(object? args)
        {
            var vm = new AuthorDetailViewModel();

            var dialog = new AddEditAuthorWindow
            {
                DataContext = vm
            };

            if (dialog.ShowDialog() == true)
            {
                await LoadAuthorRowsAsync();
            }
        }

        public async void DeleteAuthor(object? args)
        {
            if (SelectedAuthorRow is null) return;

            using var db = new BookstoreContext();

            var author = await db.Authors
                .Include(a => a.Isbns)
                .FirstAsync(a => a.AuthorId == SelectedAuthorRow.AuthorId);

            if (author.Isbns.Count > 0)
            {
                var totalBooks = author.Isbns.Count();
                MessageBox.Show(
                $"Författaren kan inte tas bort eftersom den är kopplad till {totalBooks} bok/böcker.",
                "Kan inte ta bort författare",
                 MessageBoxButton.OK,
                 MessageBoxImage.Information);

                return;
            }

            var result = MessageBox.Show(
                            $"Är du säker på att du vill ta bort \"{SelectedAuthorRow.FullName}\"?\nDetta går inte att ångra.",
                            "Radera bok",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Warning);

            if (result == MessageBoxResult.Cancel)
                return;

            
            db.Authors.Remove(author);
            await db.SaveChangesAsync();

            await LoadAuthorRowsAsync();
        }

        public bool CanDeleteAuthor(object? args)
        {
            return SelectedAuthorRow != null;
        }

        public async void EditAuthor(object? args)
        {
            if (SelectedAuthorRow is null) return;

            using var db = new BookstoreContext();

            var author = await db.Authors
                .FirstAsync(a => a.AuthorId == SelectedAuthorRow.AuthorId);

            if (author == null)
                return;

            var vm = new AuthorDetailViewModel(author);

            var dialog = new AddEditAuthorWindow
            {
                DataContext = vm
            };

            if (dialog.ShowDialog() == true)
            {
                await LoadAuthorRowsAsync();
            }
        }

        public bool CanEditAuthor(object? args)
        {
            return SelectedAuthorRow != null;
        }
    }
}
