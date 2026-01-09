using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class StockLevelRowViewModel : ViewModelBase
    {
        public string Isbn { get; }
        public string Title { get; }
        public string Author { get; }
        public decimal SalesPrice { get; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                IsModified = true;
                RaisePropertyChanged();
              
            }
        }

        private int _quantityOrdered;
        public int QuantityOrdered
        {
            get => _quantityOrdered;
            set
            {
                _quantityOrdered = value;
                IsModified = true;
                RaisePropertyChanged();
            }
        }

        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set
            {
                _isModified = value;
                RaisePropertyChanged();
                OnModifiedChanged?.Invoke();
            }
        }

        public StockLevelRowViewModel(
            string isbn,
            string title,
            string author,
            int quantity,
            int quantityOrdered,
            decimal salesPrice)
        {
            Isbn = isbn;
            Title = title;
            Author = author;
            Quantity = quantity;
            QuantityOrdered = quantityOrdered;
            SalesPrice = salesPrice;
        }

        public Action? OnModifiedChanged { get; set; }

        public void AcceptChanges()
        {
            IsModified = false;
        }
    }

}
