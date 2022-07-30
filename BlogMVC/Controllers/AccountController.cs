using BlogMVC.Data;
using BlogMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly AppDbContext context;

        public AccountController(
            SignInManager<User> signInManager,
            AppDbContext context
            )
        {
            this.signInManager = signInManager;
            this.context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsPersistent, true);
            var user = await signInManager.UserManager.FindByEmailAsync(model.UserName);
            if (result.Succeeded)
            {
                if (user.Status == Status.Disabled)
                    return RedirectToAction("Banned");
                else
                {
                    TempData["WellComePoup"] = true;
                    return Redirect(model.ReturnUrl ?? "/");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı girişi");
                return View(model);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Unauthorized()
        {
            return RedirectToAction("AccessDenied");
        }
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Ban(User user)
        {
            var result = await signInManager.UserManager.FindByEmailAsync(user.Email);
            return View(result);
        }
        public async Task<IActionResult> UnBan(User user)
        {
            var result = await signInManager.UserManager.FindByEmailAsync(user.Email);
            result.Status = Status.Enabled;
            return RedirectToAction("Index", "Users");
        }
        public async Task<IActionResult> Banned()
        {
            return View();
        }
        public async Task<IActionResult> ForgotPassword(ResetPasswordViewModel model)
        {
            var user = await signInManager.UserManager.FindByEmailAsync(model.UserName);
            model.VerifyToken = await signInManager.UserManager.GeneratePasswordResetTokenAsync(user);
            return RedirectToAction("PasswordChange",model.VerifyToken);
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(ResetPasswordViewModel model)
        {
            var user = await signInManager.UserManager.FindByEmailAsync(model.UserName);
            var result = await signInManager.UserManager.ChangePasswordAsync(user,model.OldPassword,model.NewPassword);
            return Redirect("Login");
        }
    }
}
