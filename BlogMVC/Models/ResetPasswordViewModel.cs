namespace BlogMVC.Models
{
    public class ResetPasswordViewModel
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string VerifyToken { get; set; }
    }
}
