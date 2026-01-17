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
            NewAuthorCommand = new AsyncDelegateCommand(NewAuthorAsync);
            EditAuthorCommand = new AsyncDelegateCommand(EditAuthorAsync, CanEditAuthor);
            DeleteAuthorCommand = new AsyncDelegateCommand(DeleteAuthorAsync, CanDeleteAuthor);

            AuthorRows = new ObservableCollection<AuthorRowViewModel>();
        }

        public ObservableCollection<AuthorRowViewModel> AuthorRows { get; private set; }

        public AsyncDelegateCommand EditAuthorCommand { get; }
        public AsyncDelegateCommand DeleteAuthorCommand { get; }
        public AsyncDelegateCommand NewAuthorCommand { get; }

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

        public async Task LoadAsync()
        {
            await LoadAuthorRowsAsync();
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

        public async Task NewAuthorAsync(object? args)
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

        public async Task DeleteAuthorAsync(object? args)
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

        public async Task EditAuthorAsync(object? args)
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
