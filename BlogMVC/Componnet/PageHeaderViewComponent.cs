using Microsoft.AspNetCore.Mvc;

namespace AcenteWeb.Components
{
    public class PageHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
