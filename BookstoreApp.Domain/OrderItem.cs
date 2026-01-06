using System;
using System.Collections.Generic;

namespace BookstoreApp.Infrastructure;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public int OrderItemId { get; set; }

    public string Isbn { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal PriceAtOrder { get; set; }

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
