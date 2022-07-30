using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMVC.Data
{
    public class Comment:EntityBase
    {
        public string Text { get; set; }
        public Guid BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public string Mail { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            

        }
    }
}
