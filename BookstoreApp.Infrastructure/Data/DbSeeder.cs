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
            
            if (db.Books.Any())
                return;

           
            var store1 = new Store { Street = "Drottninggatan" };
            var store2 = new Store { Street = "Kungsgatan" };

            db.Stores.AddRange(store1, store2);

          
            var fiction = new Category { Name = "Roman" };
            var classic = new Category { Name = "Klassiker" };
            var children = new Category { Name = "Barn" };

            db.Categories.AddRange(fiction, classic, children);

        
            var astrid = new Author
            {
                FirstName = "Astrid",
                Surname = "Lindgren",
                DateOfBirth = new DateOnly(1907, 11, 14)
            };

            var strindberg = new Author
            {
                FirstName = "August",
                Surname = "Strindberg",
                DateOfBirth = new DateOnly(1849, 1, 22)
            };

            var tolkien = new Author
            {
                FirstName = "J.R.R.",
                Surname = "Tolkien",
                DateOfBirth = new DateOnly(1892, 1, 3)
            };

            db.Authors.AddRange(astrid, strindberg, tolkien);


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
