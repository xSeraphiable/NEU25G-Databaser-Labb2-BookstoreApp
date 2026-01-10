using BookstoreApp.Infrastructure;
using System;
using System.Collections.Generic;
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
            IsNew = true;
            IsModified = false;

            ReleaseDate = DateOnly.FromDateTime(DateTime.Today);
        }

        // === Konstruktor: REDIGERA BEFINTLIG BOK ===
        public BookDetailViewModel(Book book)
        {
            IsNew = false;
            IsModified = false;

            Isbn = book.Isbn;
            Title = book.Title;
            SalesPrice = book.SalesPrice;
            //PurchasePrice = book.PurchasePrice;
            Weight = book.Weight;
            CategoryId = book.CategoryId;
            Category = book.Category;
            ReleaseDate = book.ReleaseDate;
            NumberOfPages = book.NumberOfPages;
        }

        // === Properties ===

        public string Isbn
        {
            get => _isbn;
            set { _isbn = value; OnChanged(); }
        }
        private string _isbn = string.Empty;

        public string Title
        {
            get => _title;
            set { _title = value; OnChanged(); }
        }
        private string _title = string.Empty;

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

        public int CategoryId
        {
            get => _categoryId;
            set { _categoryId = value; OnChanged(); }
        }
        private int _categoryId;

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
            // TODO: implementera senare
            return string.Empty;
        }
    }
}

