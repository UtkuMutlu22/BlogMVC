using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Data
{
    public class Blog : EntityBase
    {
        [Display(Name="Başlık")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string Title { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "İntro")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string InterView { get; set; }

        [Display(Name = "Taslaklara Kaydet")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public bool Draft { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public Guid CategoryId { get; set; }

        [Display(Name = "Alt Kategori")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public Guid? SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name = "Yorumlar")]
        public virtual ICollection<Comment> CommentList { get; set; } = new HashSet<Comment>();

        [Display(Name = "İlişkili Bloglar")]
        public virtual ICollection<Blog> BlogList { get; set; } = new HashSet<Blog>();
        public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
        {
            public void Configure(EntityTypeBuilder<Blog> builder)
            {
                builder
                    .HasMany(p => p.CommentList)
                    .WithOne(p => p.Blog)
                    .HasForeignKey(p => p.BlogId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder
                    .Property(p => p.Title)
                    .IsRequired();

                builder
                    .Property(p => p.InterView)
                    .IsRequired();

                builder
                    .Property(p => p.Draft)
                    .IsRequired();

                builder
                    .Property(p => p.CategoryId)
                    .IsRequired();
            }
        }
    }
}
