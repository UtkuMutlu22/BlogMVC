using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Data
{
    public class SubCategory:EntityBase
    {
        [Display(Name="Başlık")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string Title { get; set; }
        
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Kategori")]
        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();
    }
    public class SubCategoryEntityConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder
                .HasMany(p=>p.Blogs)
                .WithOne(p=>p.SubCategory)
                .HasForeignKey(p=>p.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(p => p.Title)
                .IsRequired();
        }
    }
}
