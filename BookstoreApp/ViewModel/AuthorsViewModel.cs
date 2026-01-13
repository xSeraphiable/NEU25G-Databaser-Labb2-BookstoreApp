using BookstoreApp.Commands;
using BookstoreApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class AuthorsViewModel : ViewModelBase
    {

        public AuthorsViewModel()
        {
            AuthorRows = new ObservableCollection<AuthorRowViewModel>();
            Load();
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

        private async void Load()
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

        public async void NewAuthor(object? args)
        {
            //öppna dialogruta
            //om ny författare sparas i dialog = lägg till i databas annars bara stäng ner
            //ladda authorrow igen?
        }

        public async void DeleteAuthor(object? args)
        {
            //finns en author vald?
            //finns det böcker med denna författare? i så fall informera och avbryt borttagning
            //be användaren bekräfta borttagning
            //om användaren bekräftade så ta bort författare
            //ladda authorrow igen
        }

        public bool CanDeleteAuthor(object? args)
        {
            return SelectedAuthorRow != null;
        }

        public async void EditAuthor(object? args)
        {
            //om det finns en author gå vidare
            //öppna dialogruta med vald författare
            //spara ändringar om användren sparar
            //ladda om author rows igen
        }

        public bool CanEditAuthor(object? args)
        {
            return SelectedAuthorRow != null;
        }
    }
}
