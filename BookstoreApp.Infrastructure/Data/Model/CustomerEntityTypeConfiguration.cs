using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Infrastructure;

public partial class BookstoreContext
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.CustomerId).HasName("PK_CustomerID");

            builder.Property(e => e.CustomerId).HasColumnName("CustomerID");
            builder.Property(e => e.City).HasMaxLength(50);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            builder.Property(e => e.Email).HasMaxLength(100);
            builder.Property(e => e.FirstName).HasMaxLength(100);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);
            builder.Property(e => e.PostalCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.Street).HasMaxLength(100);
            builder.Property(e => e.Surname).HasMaxLength(100);
        }
    }
}
