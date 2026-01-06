using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Infrastructure;

public partial class BookstoreContext
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => new { e.OrderId, e.OrderItemId });

            builder.Property(e => e.OrderId).HasColumnName("OrderID");
            builder.Property(e => e.OrderItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("OrderItemID");
            builder.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN");
            builder.Property(e => e.PriceAtOrder).HasColumnType("decimal(10, 2)");

            builder.HasOne(d => d.IsbnNavigation).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_ISBN");

            builder.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderItems_Orders");
        }
    }
}
