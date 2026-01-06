using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Infrastructure;

public partial class BookstoreContext
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(e => e.Name, "UQ_Categories_Name").IsUnique();

            builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
            builder.Property(e => e.Name).HasMaxLength(100);
        }
    }
}
