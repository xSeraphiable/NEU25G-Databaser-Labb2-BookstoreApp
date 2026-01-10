using BookstoreApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class BookDetailViewModel : ViewModelBase, IDataErrorInfo
    {
        public string this[string columnName] => throw new NotImplementedException(); //TODO: lägg in validering.

        public string Isbn { get; set; }
        public string Title { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public int? Weight { get; set; }
        public int CategoryId { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public int? NumberOfPages { get; set; }
        public virtual Category Category { get; set; } = null!;

        //TODO: genre behöver läggas till 

        public bool IsNew { get; }
        public bool IsModified { get; }

        public string Error => throw new NotImplementedException();
    }
}

