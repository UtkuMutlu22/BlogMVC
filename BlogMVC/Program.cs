using BlogMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), config =>
    {
        config.MigrationsAssembly("BlogMVC");
    });
});

builder.Services.AddIdentity<User, Role>(options =>
{

})
    .AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var cultureInfo = new CultureInfo("tr-TR");

CultureInfo.DefaultThreadCurrentCulture =
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern = @"dd\/MM\/yyyy";
await Init();
Sys.App = app;
app.Run();


async Task Init()
{

    using var scope = app!.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    new[]
    {
        new Role{ Name = "System", FriendlyName = "Sistem Yöneticisi"},
        new Role{ Name = "Author", FriendlyName = "Yazar"},
        new Role{ Name = "Member", FriendlyName = "Kullanıcı"},
        new Role{ Name = "Moderator", FriendlyName = "Moderatör"},

    }
    .ToList()
    .ForEach(role =>
    {
        if (!roleManager.RoleExistsAsync(role.Name).Result)
            roleManager.CreateAsync(role).Wait();
    });

    var defaultUser = await userManager.FindByNameAsync("utkumutlu034@outlook.com");
    if (defaultUser is null)
    {
        defaultUser = new User
        {
            UserName = "utkumutlu034@outlook.com",
            Email = "utkumutlu034@outlook.com",
            Name = "Built-In Admin",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(defaultUser, "!Cmos1234");
        await userManager.AddToRoleAsync(defaultUser, "System");
    }
}