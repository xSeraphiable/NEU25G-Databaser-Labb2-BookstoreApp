using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models
{
    internal class StockLevelOverview : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Isbn { get; set; } = null!;

        public string Title { get; set; }

        public string Author { get; set; }

        public int Quantity { get; set; }

        public int QuantityOrdered { get; set; }

        public decimal SalesPrice { get; set; }
    }
}
