using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Infrastructure;

public partial class BookstoreContext
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.OrderId).HasColumnName("OrderID");
            builder.Property(e => e.CustomerId).HasColumnName("CustomerID");
            builder.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.StoreId).HasColumnName("StoreID");

            builder.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");

            builder.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Orders_Store");
       
        }
    }
}
