using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Infrastructure;

public partial class BookstoreContext
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(e => e.AuthorId).HasColumnName("AuthorID");
            builder.Property(e => e.FirstName).HasMaxLength(100);
            builder.Property(e => e.Surname).HasMaxLength(100);
        }
    }
}
