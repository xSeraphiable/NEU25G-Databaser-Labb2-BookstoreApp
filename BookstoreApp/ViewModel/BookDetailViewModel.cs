using BookstoreApp.Commands;
using BookstoreApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class BookDetailViewModel : ViewModelBase, IDataErrorInfo
    {
        // === Konstruktor: NY BOK ===
        public BookDetailViewModel()
        {
            Initialize();
            IsNew = true;
            IsModified = false;

            ReleaseDate = DateOnly.FromDateTime(DateTime.Today);                 


        }

        // === Konstruktor: REDIGERA BEFINTLIG BOK ===
        public BookDetailViewModel(Book book)
        {
            Initialize();
            IsNew = false;
            IsModified = false;

            Isbn = book.Isbn;
            Title = book.Title;
            SalesPrice = book.SalesPrice;
            Language = book.Language;
            //PurchasePrice = book.PurchasePrice;
            Weight = book.Weight;
            ReleaseDate = book.ReleaseDate;
            NumberOfPages = book.NumberOfPages;
            SetCategory(book.Category);

           
        }

        public DelegateCommand SaveBookCommand { get; private set; }
        public ObservableCollection<Category> Categories { get; private set; }

        public event Action<bool>? RequestClose;

        private void Initialize()
        {
            Categories = new ObservableCollection<Category>();
            _ = LoadCategoriesAsync();

            SaveBookCommand = new DelegateCommand(SaveBookAsync);
        }

        public async void SaveBookAsync(object? args)
        {
            using var db = new BookstoreContext();

            Book book;

            if (IsNew)
            {
                book = new Book
                {
                    Isbn = Isbn
                };
                db.Books.Add(book);
            }
            else
            {
                book = await db.Books
                    .FirstOrDefaultAsync(b => b.Isbn == Isbn);

                if (book == null)
                    return;
            }


            book.Isbn = Isbn;
            book.Title = Title;
            book.SalesPrice = SalesPrice;
            book.Weight = Weight;
            book.ReleaseDate = ReleaseDate;
            book.NumberOfPages = NumberOfPages;
            book.CategoryId = Category.CategoryId;

            await db.SaveChangesAsync();

            RequestClose?.Invoke(true);
        }

        private async Task LoadCategoriesAsync()
        {
            using var db = new BookstoreContext();

            var categories = await db.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            Categories.Clear();
            foreach (var c in categories)
                Categories.Add(c);
        }

        private void SetCategory(Category? bookCategory)
        {
            if (bookCategory == null || Categories.Count == 0)
                return;

            Category = Categories.FirstOrDefault(c =>
                c.CategoryId == bookCategory.CategoryId);
        }

        public string Isbn
        {
            get => _isbn;
            set { _isbn = value; OnChanged(); }
        }
        private string _isbn = string.Empty;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnChanged();
            }
        }
        private string _title = string.Empty;

        public string Language
        {
            get => _language;
            set
            {
                _language = value;
                OnChanged();
            }
        }
        private string _language = string.Empty;

        public decimal SalesPrice
        {
            get => _salesPrice;
            set { _salesPrice = value; OnChanged(); }
        }
        private decimal _salesPrice;

        //public decimal PurchasePrice
        //{
        //    get => _purchasePrice;
        //    set { _purchasePrice = value; OnChanged(); }
        //}
        //private decimal _purchasePrice;

        public int? Weight
        {
            get => _weight;
            set { _weight = value; OnChanged(); }
        }
        private int? _weight;

        public DateOnly? ReleaseDate
        {
            get => _releaseDate;
            set { _releaseDate = value; OnChanged(); }
        }
        private DateOnly? _releaseDate;

        public int? NumberOfPages
        {
            get => _numberOfPages;
            set { _numberOfPages = value; OnChanged(); }
        }
        private int? _numberOfPages;

        public Category Category
        {
            get => _category;
            set { _category = value; OnChanged(); }
        }
        private Category _category = null!;

        // === State ===
        public bool IsNew { get; }
        public bool IsModified { get; private set; }

        // === Validation ===
        public string this[string columnName] => Validate(columnName);
        public string Error => string.Empty;

        private void OnChanged([CallerMemberName] string? name = null)
        {
            IsModified = true;
            RaisePropertyChanged(name);
            RaisePropertyChanged(nameof(IsModified));
        }

        private string Validate(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Isbn):
                    if (string.IsNullOrWhiteSpace(Isbn))
                    {
                        return "ISBN måste anges";
                    }
                    if (Isbn.Length != 13)
                    {
                        return "ISBN måste vara exakt 13 tecken";
                    }
                    if (!Isbn.All(char.IsDigit))
                    {
                        return "ISBN får endast innehålla siffror";
                    }
                    break;
                case nameof(Title):
                    if (string.IsNullOrWhiteSpace(Title))
                    {
                        return "Titel måste anges";
                    }
                    if (Title.Length > 200)
                    {
                        return "Titeln är för lång (max 200 tecken)";
                    }
                    break;
                case nameof(NumberOfPages):
                    if (NumberOfPages is null)
                    {
                        return string.Empty;
                    }
                    if (NumberOfPages <= 0)
                    {
                        return "Antal sidor måste vara större än 0";
                    }
                    if (NumberOfPages > 10000)
                    {
                        return "Ange antal sidor under 10 000";
                    }
                    break;
                case nameof(Language):
                    if (Language is null)
                    {
                        return string.Empty;
                    }
                    if (Language.Length > 50)
                    {
                        return "Texten är för lång (max 50 tecken)";
                    }
                    break;
                case nameof(Weight):
                    if (Weight is null)
                    {
                        return string.Empty;
                    }
                    if (Weight <= 0)
                    {
                        return "Vikt måste vara större än 0";
                    }
                    if (Weight > 10000)
                    {
                        return "Ange en vikt under 10 000 gram";
                    }
                    break;
                case nameof(Category):
                    if (Category == null)
                        return "Kategori måste väljas";
                    break;

            }

            return string.Empty;
        }
    }
}

