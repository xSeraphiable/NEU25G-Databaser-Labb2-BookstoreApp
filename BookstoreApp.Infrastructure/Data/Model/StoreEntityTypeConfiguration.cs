using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Infrastructure;

public partial class BookstoreContext
{
    public class StoreEntityTypeConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasIndex(e => new { e.Street, e.PostalCode, e.City }, "UQ_Store_Adress").IsUnique();

            builder.Property(e => e.City).HasMaxLength(50);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);
            builder.Property(e => e.PostalCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.Street).HasMaxLength(100);
        }
    }
}
