using System;
using System.Collections.Generic;

namespace BookstoreApp.Infrastructure;

public partial class StockLevel
{
    public string Isbn { get; set; } = null!;

    public int StoreId { get; set; }

    public int Quantity { get; set; }

    public int QuantityOrdered { get; set; }

    public decimal PurchasePrice { get; set; }

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
