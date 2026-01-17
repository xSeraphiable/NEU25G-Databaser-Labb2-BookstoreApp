using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(BookstoreContext db)
        {
            // STORES
            if (!db.Stores.Any())
            {
                db.Stores.AddRange(
                    new Store { Street = "Drottninggatan", PostalCode = "11151", City = "Stockholm" },
                    new Store { Street = "Kungsgatan", PostalCode = "40014", City = "Göteborg" }
                );
                await db.SaveChangesAsync();
            }

            // CATEGORIES
            if (!db.Categories.Any())
            {
                db.Categories.AddRange(
                    new Category { Name = "Roman" },
                    new Category { Name = "Klassiker" },
                    new Category { Name = "Barn" }
                );
                await db.SaveChangesAsync();
            }

            // AUTHORS
            if (!db.Authors.Any())
            {
                db.Authors.AddRange(
                    new Author
                    {
                        FirstName = "Astrid",
                        Surname = "Lindgren",
                        DateOfBirth = new DateOnly(1907, 11, 14)
                    },
                    new Author
                    {
                        FirstName = "August",
                        Surname = "Strindberg",
                        DateOfBirth = new DateOnly(1849, 1, 22)
                    },
                    new Author
                    {
                        FirstName = "J.R.R.",
                        Surname = "Tolkien",
                        DateOfBirth = new DateOnly(1892, 1, 3)
                    }
                );
                await db.SaveChangesAsync();
            }

            // BOOKS + BOOKAUTHOR
            if (!db.Books.Any())
            {
                var children = await db.Categories.FirstAsync(c => c.Name == "Barn");
                var classic = await db.Categories.FirstAsync(c => c.Name == "Klassiker");
                var fiction = await db.Categories.FirstAsync(c => c.Name == "Roman");

                var astrid = await db.Authors.FirstAsync(a => a.Surname == "Lindgren");
                var strindberg = await db.Authors.FirstAsync(a => a.Surname == "Strindberg");
                var tolkien = await db.Authors.FirstAsync(a => a.Surname == "Tolkien");

                var book1 = new Book
                {
                    Isbn = "9789129688313",
                    Title = "Bröderna Lejonhjärta",
                    SalesPrice = 179,
                    Category = children,
                    ReleaseDate = new DateOnly(1973, 1, 1)
                };
                book1.Authors.Add(astrid);

                var book2 = new Book
                {
                    Isbn = "9789100120851",
                    Title = "Röda rummet",
                    SalesPrice = 149,
                    Category = classic,
                    ReleaseDate = new DateOnly(1879, 1, 1)
                };
                book2.Authors.Add(strindberg);

                var book3 = new Book
                {
                    Isbn = "9780261102385",
                    Title = "The Hobbit",
                    SalesPrice = 199,
                    Category = fiction,
                    ReleaseDate = new DateOnly(1937, 9, 21)
                };
                book3.Authors.Add(tolkien);

                db.Books.AddRange(book1, book2, book3);
                await db.SaveChangesAsync();
            }

            // STOCK LEVELS
            if (!db.StockLevels.Any())
            {
                var store1 = await db.Stores.FirstAsync(s => s.City == "Stockholm");
                var store2 = await db.Stores.FirstAsync(s => s.City == "Göteborg");

                var book1 = await db.Books.FirstAsync(b => b.Title == "Bröderna Lejonhjärta");
                var book2 = await db.Books.FirstAsync(b => b.Title == "Röda rummet");
                var book3 = await db.Books.FirstAsync(b => b.Title == "The Hobbit");

                db.StockLevels.AddRange(
                    new StockLevel
                    {
                        StoreId = store1.StoreId,
                        Isbn = book1.Isbn,
                        Quantity = 5,
                        QuantityOrdered = 1
                    },
                    new StockLevel
                    {
                        StoreId = store1.StoreId,
                        Isbn = book2.Isbn,
                        Quantity = 2,
                        QuantityOrdered = 0
                    },
                    new StockLevel
                    {
                        StoreId = store2.StoreId,
                        Isbn = book3.Isbn,
                        Quantity = 0,
                        QuantityOrdered = 4
                    }
                );

                await db.SaveChangesAsync();
            }
        }
    }

}
