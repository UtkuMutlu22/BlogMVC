using BlogMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogMVC.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetData(DataTableParameters parameters)
        {
            var query = context
                .Rosettes
                .AsNoTracking()
                .Where(p =>
                    (parameters.Search.Value == null || (p.Title != null && p.Title.ToLower().Contains(parameters.Search.Value.ToLower())))
                 );

            switch (parameters.Columns[parameters.Order[0].Column].Data)
            {
                case "title":
                default:
                    query = parameters.Order[0].Dir == DataTableOrderDir.ASC ? query.OrderBy(p => p.Title) : query.OrderByDescending(p => p.Title);
                    break;

            }

            var result = new DataTableResult
            {
                draw = parameters.Draw,
                data = await query
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Enabled,
                    Date = p.DateCreated.ToShortDateString(),
                    UserName = p.User.Name,
                    p.UserId,
                })
                .ToListAsync(),
                recordsFiltered = query.Count(),
                recordsTotal = context.Rosettes.Count(),

            };
            return Json(result);
        }
        public async Task<IActionResult> Create()
        {
            return View(new Rosette { Enabled = Status.Enabled });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rosette model)
        {
            model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.DateCreated = DateTime.Now;
            model.Enabled = Status.Enabled;
            using var ms = new MemoryStream();
            foreach (var item in model.DocumentFiles)
            {
                await item.CopyToAsync(ms);
                model.Image = ms.ToArray();
            }

            context.Rosettes.Add(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = $"{_entityName} ekleme işlemi başarıyla tamamlanmıştır!";
            }
            catch (DbUpdateException)
            {

                TempData["error"] = $"{model.Title} isimli kayıt,daha önce eklenmiş olduğundan dolayı ekleme işlemi tamamlanamıyor!";
                return View(model);
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Remove(Guid id)
        {
            var model = await context.Categories.FindAsync(id);
            context.Categories.Remove(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = $"{_entityName} silme işlemi başarıyla tamamlanmıştır!";
            }
            catch (DbUpdateException)
            {
                TempData["error"] = $"{model.Title} isimli kayıt, bir ya da daha fazla kayıt ile ilişkili olduğundan dolayı silme işlemi tamamlanamıyor!";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {

            var model = await context.Categories.FindAsync(id);
            ViewBag.Transactions = await context.Transactions.Where(p => p.EntityName == nameof(Category) && p.ItemId == id).ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
            context.Categories.Update(model);
            await context.SaveChangesAsync();
            TempData["success"] = $"{_entityName} güncelleme işlemi başarıyla tamamlanmıştır!";
            return RedirectToAction(nameof(Index));
        }
    }
}
