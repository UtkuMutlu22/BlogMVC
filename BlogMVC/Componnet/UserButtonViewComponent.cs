using BlogMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Componnet
{
    public class UserButtonViewComponent : ViewComponent
    {
        private readonly UserManager<User> userManager;

        public UserButtonViewComponent(
            UserManager<User> userManager
            )
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var model = userManager.FindByNameAsync(User.Identity!.Name).Result;
            return View(model);
        }
    }
}
