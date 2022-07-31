using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Controllers
{
    public class RossetteController : Controller
    {
        private readonly AppDbContext context;
        private readonly string _entityName = "Rozetler";

        public RossetteController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
