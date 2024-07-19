using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockWebApp1.Models;

namespace StockWebApp1
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Content> Contents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContentTag>()
                .HasKey(ct => new { ct.ContentId, ct.TagId });

            modelBuilder.Entity<ContentTag>()
                .HasOne(ct => ct.Content)
                .WithMany(c => c.ContentTags)
                .HasForeignKey(ct => ct.ContentId);

            modelBuilder.Entity<ContentTag>()
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.ContentTags)
                .HasForeignKey(ct => ct.TagId);

            modelBuilder.Entity<User>()
               .HasMany(u => u.Subscriptions)
               .WithMany(u => u.Subscribers)
               .UsingEntity<Dictionary<string, object>>(
                   "UserSubscription",
                   j => j.HasOne<User>().WithMany().HasForeignKey("SubscriberId"),
                   j => j.HasOne<User>().WithMany().HasForeignKey("SubscriptionId"));
        }

    }
}
