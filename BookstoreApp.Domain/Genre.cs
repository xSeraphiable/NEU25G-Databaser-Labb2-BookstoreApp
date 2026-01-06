using System;
using System.Collections.Generic;

namespace BookstoreApp.Infrastructure;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Isbns { get; set; } = new List<Book>();
}
