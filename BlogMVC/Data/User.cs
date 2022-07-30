using AcenteWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMVC.Data
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public byte[]? Image { get; set; }
        public string? Description { get; set; }
        public string? Twitter { get; set; }
        public string? Linkledn { get; set; }
        public string? Github { get; set; }
        public string? WebSite { get; set; }
        public string? Country { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<User> FollowList { get; set; } = new HashSet<User>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        public virtual ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Rosette> Rosettes { get; set; } = new HashSet<Rosette>();


    }
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(p => p.Transactions)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.Blogs)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.Categories)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.Comments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasMany(p => p.Rosettes)
              .WithOne(p => p.User)
              .HasForeignKey(p => p.UserId)
              .OnDelete(DeleteBehavior.Restrict);
        }

    }
}


