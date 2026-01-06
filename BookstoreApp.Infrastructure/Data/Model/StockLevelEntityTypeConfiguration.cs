using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Infrastructure;

public partial class BookstoreContext
{
    public class StockLevelEntityTypeConfiguration : IEntityTypeConfiguration<StockLevel>
    {
        public void Configure(EntityTypeBuilder<StockLevel> builder)
        {
            builder.HasKey(e => new { e.Isbn, e.StoreId });

            builder.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN");
            builder.Property(e => e.StoreId).HasColumnName("StoreID");
            builder.Property(e => e.PurchasePrice).HasColumnType("decimal(10, 2)");

            builder.HasOne(d => d.IsbnNavigation).WithMany(p => p.StockLevels)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockLevels_ISBN");

            builder.HasOne(d => d.Store).WithMany(p => p.StockLevels)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockLevels_Store");
        }
    }
}
