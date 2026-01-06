using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Infrastructure;

public partial class BookstoreContext
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.Isbn);

            builder.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN");
            builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
            builder.Property(e => e.Language).HasMaxLength(50);
            builder.Property(e => e.SalesPrice).HasColumnType("decimal(10, 2)");
            builder.Property(e => e.Title).HasMaxLength(200);

            builder.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Category");

            builder.HasMany(d => d.Authors).WithMany(p => p.Isbns)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookAuthor_Authors"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookAuthor_ISBN"),
                    j =>
                    {
                        j.HasKey("Isbn", "AuthorId");
                        j.ToTable("BookAuthor");
                        j.IndexerProperty<string>("Isbn")
                            .HasMaxLength(13)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("ISBN");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("AuthorID");
                    });

            builder.HasMany(d => d.Genres).WithMany(p => p.Isbns)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_BookGenre_Genre"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn")
                        .HasConstraintName("FK_BookGenre_Books"),
                    j =>
                    {
                        j.HasKey("Isbn", "GenreId");
                        j.ToTable("BookGenre");
                        j.IndexerProperty<string>("Isbn")
                            .HasMaxLength(13)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("ISBN");
                        j.IndexerProperty<int>("GenreId").HasColumnName("GenreID");
                    });
        }
    }
}
