using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMVC.Data
{
    public class Category:EntityBase
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();
    }
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
               .HasIndex(p => new { p.Title })
               .IsUnique(false);

            builder
                .HasMany(p => p.Blogs)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
