using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Infrastructure;

public partial class BookstoreContext
{
    public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genre");

            builder.HasIndex(e => e.Name, "UQ_Genre_Name").IsUnique();

            builder.Property(e => e.GenreId).HasColumnName("GenreID");
            builder.Property(e => e.Name).HasMaxLength(100);
        }
    }
}
