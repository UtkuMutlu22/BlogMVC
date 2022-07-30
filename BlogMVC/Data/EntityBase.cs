using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Data
{
    public enum Status
    {
        [Display(Name = "Hayır")]
        Disabled,

        [Display(Name = "Evet")]
        Enabled

    }
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        [Display(Name = "Ekleme Tarihi")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Display(Name = "Aktif")]
        public Status Enabled { get; set; } = Status.Enabled;

        [Display(Name = "Ekleyen")]
        public virtual Guid UserId { get; set; }

        public virtual User User { get; set; }


    }
}
