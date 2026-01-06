using System;
using System.Collections.Generic;

namespace BookstoreApp.Infrastructure;

public partial class Book
{
    public string Isbn { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Language { get; set; }

    public int? Weight { get; set; }

    public int CategoryId { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? NumberOfPages { get; set; }

    public decimal SalesPrice { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<StockLevel> StockLevels { get; set; } = new List<StockLevel>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
