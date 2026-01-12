using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BookstoreApp.Infrastructure;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public virtual ICollection<Book> Isbns { get; set; } = new List<Book>();

}
