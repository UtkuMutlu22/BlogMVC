using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMVC.Data
{
    public class Blog:EntityBase
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string InterView { get; set; }
        public bool Draft { get; set; } 
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> CommentList { get; set; } = new HashSet<Comment>();
        public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
        {
            public void Configure(EntityTypeBuilder<Blog> builder)
            {
                builder
                    .HasMany(p => p.CommentList)
                    .WithOne(p => p.Blog)
                    .HasForeignKey(p => p.BlogId)
                    .OnDelete(DeleteBehavior.Restrict);

            }
        }
    }
}
