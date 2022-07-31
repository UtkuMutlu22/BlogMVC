using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogMVC.Data
{
    public class Rosette : EntityBase
    {
        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string Title { get; set; }

        [Display(Name = "İçerik")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string Description { get; set; }

        [Display(Name = "Resim")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public byte[] Image { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

        [NotMapped]
        public IEnumerable<IFormFile>? DocumentFiles { get; set; }


    }
    public class RossetteEntityTypeConfiguration : IEntityTypeConfiguration<Rosette>
    {
        public void Configure(EntityTypeBuilder<Rosette> builder)
        {

            builder
                .Property(p => p.Title)
                .IsRequired();

            builder
                .Property(p => p.Description)
                .IsRequired();

            builder
                .Property(p => p.Image)
                .IsRequired();
        }
    }
}
