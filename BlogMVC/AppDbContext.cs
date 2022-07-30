using AcenteWeb.Models;
using BlogMVC;
using BlogMVC.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Claims;
public class AppDbContext : IdentityDbContext<User, Role, Guid>
{

    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(config =>
        {
            config.Ignore();
        });
        base.OnConfiguring(optionsBuilder);
    }

    public override int SaveChanges()
    {
        //AddToTransactions();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //AddToTransactions();
        return base.SaveChangesAsync(cancellationToken);
    }
    //private void AddToTransactions()
    //{

    //    using var scope = Sys.App!.Services.CreateScope();
    //    var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

    //    ChangeTracker.Entries().ToList().ForEach(p =>
    //    {
    //        if (p.Entity is EntityBase)
    //        {
    //            var commandText = @"insert into Transactions 
    //                    (Id, DateTime, UserId,ItemId) 
    //                    values 
    //                    (newid(), @DateTime, @ItemId);";

    //            Guid itemId = Guid.Empty;

    //            p.CurrentValues.TryGetValue("Id", out itemId);

    //            Database.ExecuteSqlRaw(commandText,
    //                new SqlParameter("@DateTime", DateTime.Now),
    //                new SqlParameter("@UserId", httpContextAccessor.HttpContext!.User?.FindFirst(ClaimTypes.NameIdentifier)!.Value),
    //                new SqlParameter("@ItemId", itemId));
    //        }
    //    });

    //}

    public virtual DbSet<Blog> Blogs { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Rosette> Rosettes { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }

}