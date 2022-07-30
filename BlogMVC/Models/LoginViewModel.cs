using BlogMVC.Data;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Models
{
    public class LoginViewModel
    {
        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Display(Name = "Parola")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
        public string? ReturnUrl { get; set; }
        public Status Status { get; set; }
    }
}
