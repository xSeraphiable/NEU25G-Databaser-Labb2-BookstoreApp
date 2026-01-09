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

        public void AcceptChanges()
        {
            IsModified = false;
        }
    }

}
