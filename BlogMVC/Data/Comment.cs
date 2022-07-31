using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Data
{
    public class Comment:EntityBase
    {

        [Display(Name="İçerik")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string Text { get; set; }

        [Display(Name ="Blog")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public Guid BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        [Display(Name ="Mail Adresi")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string Mail { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            builder
                .Property(p => p.Text)
                .IsRequired();

            builder
                .Property(p => p.BlogId)
                .IsRequired();
        }
    }
}
