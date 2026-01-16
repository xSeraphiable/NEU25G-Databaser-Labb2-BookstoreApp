using BookstoreApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class StockLevelRowViewModel : ViewModelBase, IDataErrorInfo
    {
        public string Isbn { get; }
        public string Title { get; }
        public string Authors { get; }
        public decimal SalesPrice { get; }

        public int OriginalQuantity { get; }
        public int OriginalQuantityOrdered { get; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity == value) return;

                _quantity = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsModified));
                OnModifiedChanged?.Invoke();
            }
        }

        private int _quantityOrdered;
        public int QuantityOrdered
        {
            get => _quantityOrdered;
            set
            {
                if(_quantityOrdered == value) return;

                _quantityOrdered = value;         
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsModified));
                OnModifiedChanged?.Invoke();
            }
        }

        public bool IsModified =>
        Quantity != OriginalQuantity ||
        QuantityOrdered != OriginalQuantityOrdered;

        public void Reset()
        {
            Quantity = OriginalQuantity;
            QuantityOrdered = OriginalQuantityOrdered;
        }
        public StockLevelRowViewModel(
            string isbn,
            string title,
            string authors,
            int quantity,
            int quantityOrdered,
            decimal salesPrice)
        {
            Isbn = isbn;
            Title = title;
            Authors = authors;
            SalesPrice = salesPrice;

            OriginalQuantity = quantity;
            OriginalQuantityOrdered = quantityOrdered;

            _quantity = quantity;
            _quantityOrdered = quantityOrdered;
        }
        
        public Action? OnModifiedChanged { get; set; }
        public bool HasErrors =>
    !string.IsNullOrEmpty(this[nameof(Quantity)]) ||
    !string.IsNullOrEmpty(this[nameof(QuantityOrdered)]);

        public string Error => null!;

        public string this[string propertyName]
        {
            get
            {
                if (propertyName == nameof(Quantity))
                {
                    if (Quantity < 0)
                        return "Antal kan inte vara negativt";

                    if (Quantity > 10000)
                        return "Antal kan inte vara högre än 9999";
                }

                if (propertyName == nameof(QuantityOrdered))
                {
                    if (QuantityOrdered < 0)
                        return "Beställt antal kan inte vara negativt";

                    if (QuantityOrdered > 10000)
                        return "Beställt antal kan inte vara högre än 9999";
                }

                return string.Empty; 
            }
        }

    }

}
