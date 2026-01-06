using System;
using System.Collections.Generic;

namespace BookstoreApp.Infrastructure;

public partial class Store
{
    public int StoreId { get; set; }

    public string Street { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StockLevel> StockLevels { get; set; } = new List<StockLevel>();
}
